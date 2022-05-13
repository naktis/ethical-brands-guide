using Data.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Dto.InputDto
{
    public class RatingEntryInDto
    {
        public int? UserId { get; set; }
        public int CompanyId { get; set; }

        public int PlanetRating { get; set; }
        public int PeopleRating { get; set; }
        public int AnimalsRating { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        public RatingEntry GetRatingEntry()
        {
            return new RatingEntry
            {
                UserId = UserId,
                PlanetRating = PlanetRating,
                AnimalsRating = AnimalsRating,
                PeopleRating = PeopleRating
            };
        }
    }
}
