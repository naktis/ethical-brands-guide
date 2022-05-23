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

        public RatingOutDto() { }
        public RatingOutDto(Rating entity)
        {
            PlanetRating = entity.PlanetRating;
            PeopleRating = entity.PeopleRating;
            AnimalsRating = entity.AnimalsRating;
            TotalRating = entity.TotalRating;
            Description = entity.Description;
        }

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
