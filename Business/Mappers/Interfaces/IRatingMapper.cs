using Business.Dto.InputDto;
using Business.Dto.OutputDto;
using Data.Models;

namespace Business.Mappers.Interfaces
{
    public interface IRatingMapper : IMapper<Rating, RatingInDto>
    {
        public RatingOutDto EntityToDto(Rating entity);
    }
}
