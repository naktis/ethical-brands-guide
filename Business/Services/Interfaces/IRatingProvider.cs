using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using System.Threading.Tasks;

namespace Business.Services.Interfaces
{
    public interface IRatingProvider
    {
        public Task<CreatedItemOutDto> AddAsync(RatingEntryInDto rating);
        public Task<RatingOutDto> GetGuestRatingAsync(int companyId);
    }
}
