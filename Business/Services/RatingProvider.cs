using Business.Calculators;
using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Data.Context;
using Data.Models;
using System;
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
            var ratingEntry = ratingDto.GetRatingEntry();
            ratingEntry.RatingId = (await _context.Companies.FindAsync(ratingDto.CompanyId)).RatingId;
            var savedRatingEntry = await _context.RatingEntries.AddAsync(ratingEntry);

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

        public async Task<RatingOutDto> GetGuestRatingAsync(int companyId)
        {
            var ratingId = (await _context.Companies.FindAsync(companyId)).RatingId;
            var guestRatings = _context.RatingEntries
                .Where(re => re.RatingId == ratingId && re.UserId == null);

            var guestRatingsDto = new RatingOutDto
            {
                AnimalsRating = Math.Round(guestRatings.Select(x => x.AnimalsRating).Average(), 2),
                PlanetRating = Math.Round(guestRatings.Select(x => x.PlanetRating).Average(), 2),
                PeopleRating = Math.Round(guestRatings.Select(x => x.PeopleRating).Average(), 2)
            };
            guestRatingsDto.TotalRating = _calculator.GetTotal(guestRatingsDto.ToEntity());
            
            return guestRatingsDto;
        }
    }
}
