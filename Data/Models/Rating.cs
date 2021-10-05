namespace Data.Models
{
    public class Rating
    {
        public int RatingId { get; set; }
        public int PlanetRating { get; set; }
        public int PeopleRating { get; set; }
        public int AnimalsRating { get; set; }
        public double TotalRating { get; set; }
        public string Description { get; set; }

        public Company Company { get; set; }
    }
}
