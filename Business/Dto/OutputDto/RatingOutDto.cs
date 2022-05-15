using Data.Models;

namespace Business.Dto.OutputDto
{
    public class RatingOutDto
    {
        public double PlanetRating { get; set; }
        public double PeopleRating { get; set; }
        public double AnimalsRating { get; set; }
        public string Description { get; set; }
        public double TotalRating { get; set; }

        public Rating ToEntity()
        {
            return new Rating
            {
                PlanetRating = PlanetRating,
                PeopleRating = PeopleRating,
                AnimalsRating = AnimalsRating
            };
        }
    }
}
