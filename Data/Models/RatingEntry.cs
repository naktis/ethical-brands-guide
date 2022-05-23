namespace Data.Models
{
    public class RatingEntry
    {
        public int RatingEntryId { get; set; }

        public int PlanetRating { get; set; }
        public int PeopleRating { get; set; }
        public int AnimalsRating { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }

        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        public Comment Comment { get; set; }
    }
}
