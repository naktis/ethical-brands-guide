using Data.Models;

namespace Business.Dto.InputDto
{
    public class RatingInDto
    {
        public int PlanetRating { get; set; }
        public int PeopleRating { get; set; }
        public int AnimalsRating { get; set; }
        public string Description { get; set; }

        public RatingEntry ToEntity()
        {
            return new RatingEntry
            {
                PlanetRating = PlanetRating,
                PeopleRating = PeopleRating,
                AnimalsRating = AnimalsRating
            };
        }
    }
}
