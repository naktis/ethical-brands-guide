using System.Collections.Generic;

namespace Data.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public double PlanetRating { get; set; }
        public double PeopleRating { get; set; }
        public double AnimalsRating { get; set; }
        public double TotalRating { get; set; }
        public string Description { get; set; }

        public Company Company { get; set; }
        public ICollection<RatingEntry> RatingEntries { get; set; }
    }
}
