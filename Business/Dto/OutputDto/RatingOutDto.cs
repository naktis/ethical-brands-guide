using Business.Dto.InputDto;

namespace Business.Dto.OutputDto
{
    public class RatingOutDto : RatingInDto
    {
        public int RatingId { get; set; }

        public double TotalRating { get; set; }
    }
}
