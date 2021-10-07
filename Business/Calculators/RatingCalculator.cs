using Data.Models;
using System;

namespace Business.Calculators
{
    public class RatingCalculator : IRatingCalculator
    {
        public double GetTotal(Rating rating)
        {
            return Math.Round(((double)(rating.AnimalsRating + rating.PeopleRating + 
                rating.PlanetRating) / 3), 2);
        }
    }
}
