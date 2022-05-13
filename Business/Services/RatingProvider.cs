using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Business.Services.Interfaces;
using Data.Context;
using Data.Models;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RatingProvider : IRatingProvider
    {
        private readonly AppDbContext _context;

        public RatingProvider(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CreatedItemOutDto> AddAsync(RatingEntryInDto ratingDto)
        {
            var ratingEntry = ratingDto.GetRatingEntry();
            ratingEntry.RatingId = (await _context.Companies.FindAsync(ratingDto.CompanyId)).RatingId;
            var savedRatingEntry = await _context.RatingEntries.AddAsync(ratingEntry);

            await _context.Comments.AddAsync(new Comment
            {
                Text = ratingDto.Comment,
                Entry = savedRatingEntry.Entity,
                Approved = (await _context.Users.FindAsync(ratingDto.UserId)).Type == UserType.Admin
            });
            await _context.SaveChangesAsync();

            return new CreatedItemOutDto { Id = savedRatingEntry.Entity.RatingEntryId };
        }
    }
}
