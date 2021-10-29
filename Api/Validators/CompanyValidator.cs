using Api.Validators.Interfaces;
using Business.Dto.InputDto;

namespace Api.Validators
{
    public class CompanyValidator : ICompanyValidator
    {
        const int MinNameLength = 2;
        const int MaxNameLength = 50;
        const int MaxDescriptionLength = 500;
        const int MaxRating = 5;
        public bool Validate(CompanyInDto company)
        {
            if (TextGoodLength(company.Name, MaxNameLength, MinNameLength) &&
                TextGoodLength(company.Description, MaxDescriptionLength) &&
                TextGoodLength(company.Rating.Description, MaxDescriptionLength) &&
                RatingValid(company.Rating.AnimalsRating) && 
                RatingValid(company.Rating.PeopleRating) &&
                RatingValid(company.Rating.PlanetRating))
                return true;

            return false;
        }

        private bool TextGoodLength(string text, int max, int min = 0)
        {
            return text.Length >= min && text.Length <= max;
        }

        private bool RatingValid(int rating)
        {
            return rating >= 1 && rating <= MaxRating;
        }
    }
}
