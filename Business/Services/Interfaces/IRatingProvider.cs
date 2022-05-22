using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IRatingProvider
    {
        public Task<CreatedItemOutDto> AddAsync(RatingEntryInDto rating);
        public Task<RatingOutDto> GetGuestRatingAsync(int companyId);
        public Task<RatingOutDto> GetExpertRatingAsync(int companyId);
        public Task<IEnumerable<CommentOutDto>> GetCommentsAsync(int companyId);
        public Task<bool> CommentExistsAsync(int commentId);
        public Task UpdateCommmentAsync(int commentId, bool approved);
    }
}
