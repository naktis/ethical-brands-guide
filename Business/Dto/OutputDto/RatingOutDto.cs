using Business.Dto.InputDto;

namespace Business.Dto.OutputDto
{
    public class RatingOutDto : RatingInDto
    {
        public double TotalRating { get; set; }
    }
}
