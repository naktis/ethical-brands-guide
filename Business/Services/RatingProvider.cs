using Business.Calculators;
using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Data.Context;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RatingProvider : IRatingProvider
    {
        private readonly AppDbContext _context;
        private readonly IRatingCalculator _calculator;

        public RatingProvider(AppDbContext context, IRatingCalculator calculator)
        {
            _context = context;
            _calculator = calculator;
        }
        public async Task<CreatedItemOutDto> AddAsync(RatingEntryInDto ratingDto)
        {
            var ratingId = (await _context.Companies.FindAsync(ratingDto.CompanyId)).RatingId;

            var ratingEntry = ratingDto.GetEntity();
            ratingEntry.RatingId = ratingId;
            var savedRatingEntry = await _context.RatingEntries.AddAsync(ratingEntry);
            await _context.SaveChangesAsync();

            RecalculateGeneralRatingAsync(ratingEntry.RatingId);

            if (ratingDto.Comment is not null)
            {
                await _context.Comments.AddAsync(new Comment
                {
                    Text = ratingDto.Comment,
                    Entry = savedRatingEntry.Entity,
                    Approved = ratingDto.UserId is not null
                });
            }

            await _context.SaveChangesAsync();

            return new CreatedItemOutDto { Id = savedRatingEntry.Entity.RatingEntryId };
        }

        public async Task<IEnumerable<CommentOutDto>> GetCommentsAsync(int companyId)
        {
            var ratingId = (await _context.Companies.FindAsync(companyId)).RatingId;
            var ratings = _context.RatingEntries
                .Where(re => re.RatingId == ratingId)
                .Select(re => re.RatingEntryId);
            var comments = _context.Comments.Where(c => ratings.Contains(c.EntryId));

            var commentsDto = new List<CommentOutDto>();
            foreach (var c in comments)
            {
                    commentsDto.Add(new CommentOutDto
                    {
                        Id = c.CommentId,
                        Text = c.Text,
                        Approved = c.Approved
                    });
            }
            return commentsDto;
        }

        public async Task<RatingOutDto> GetExpertRatingAsync(int companyId)
        {
            var ratingId = (await _context.Companies.FindAsync(companyId)).RatingId;
            var expertRatings = _context.RatingEntries
                .Where(re => re.RatingId == ratingId && re.UserId != null);

            return expertRatings.Any() ? AggregateRatings(expertRatings) :
                new RatingOutDto(await _context.Ratings.FindAsync(ratingId));
        }

        public async Task<RatingOutDto> GetGuestRatingAsync(int companyId)
        {
            var ratingId = (await _context.Companies.FindAsync(companyId)).RatingId;
            var guestRatings = _context.RatingEntries
                .Where(re => re.RatingId == ratingId && re.UserId == null);
            return guestRatings.Any() ? AggregateRatings(guestRatings) : new RatingOutDto();
        }

        public async Task<bool> CommentExistsAsync(int commentId)
        {
            return await _context.Comments.FindAsync(commentId) != null;
        }

        public async Task UpdateCommmentAsync(int commentId, bool approved)
        {
            var comment = await _context.Comments.FindAsync(commentId);

            if (approved)
                comment.Approved = approved;
            else
                _context.Comments.Remove(comment);         
            
            await _context.SaveChangesAsync();
        }

        private RatingOutDto AggregateRatings(IQueryable<RatingEntry> ratings)
        {
            var aggregatedRatings = new RatingOutDto
            {
                AnimalsRating = Math.Round(ratings.Select(x => x.AnimalsRating).Average(), 2),
                PlanetRating = Math.Round(ratings.Select(x => x.PlanetRating).Average(), 2),
                PeopleRating = Math.Round(ratings.Select(x => x.PeopleRating).Average(), 2)
            };

            aggregatedRatings.TotalRating = _calculator.GetTotal(aggregatedRatings.ToEntity());
            return aggregatedRatings;
        }

        private async void RecalculateGeneralRatingAsync(int ratingId)
        {
            var rating = await _context.Ratings.FindAsync(ratingId);

            var animalsExpertAvg = _context.RatingEntries.Where(x => x.UserId != null).Average(x => x.AnimalsRating);
            var animalsGuestAvg = _context.RatingEntries.Where(x => x.UserId == null).Average(x => x.AnimalsRating);
            if (animalsGuestAvg == 0)
            {
                rating.AnimalsRating = Math.Round(animalsExpertAvg, 2);
            }
            else
            {
                rating.AnimalsRating = Math.Round((animalsExpertAvg + animalsGuestAvg) / 2, 2);
            }

            var peopleExpertAvg = _context.RatingEntries.Where(x => x.UserId != null).Average(x => x.PeopleRating);
            var peopleGuestAvg = _context.RatingEntries.Where(x => x.UserId == null).Average(x => x.PeopleRating);
            if (peopleGuestAvg == 0)
            {
                rating.PeopleRating = Math.Round(peopleExpertAvg, 2);
            }
            else
            {
                rating.PeopleRating = Math.Round((peopleExpertAvg + peopleGuestAvg) / 2, 2);
            }

            var planetExpertAvg = _context.RatingEntries.Where(x => x.UserId != null).Average(x => x.PlanetRating);
            var planetGuestAvg = _context.RatingEntries.Where(x => x.UserId == null).Average(x => x.PlanetRating);
            if (peopleGuestAvg == 0)
            {
                rating.PlanetRating = Math.Round(peopleExpertAvg, 2);
            }
            else
            {
                rating.PlanetRating = Math.Round((peopleExpertAvg + peopleGuestAvg) / 2, 2);
            }

            rating.TotalRating = _calculator.GetTotal(rating);
        }
    }
}
