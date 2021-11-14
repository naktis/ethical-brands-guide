using Api.RequestProcessors.Validators.Interfaces;
using Business.Dto.InputDto;

namespace Api.Validators
{
    public class CompanyValidator : ICompanyValidator
    {
        const int MinNameLength = 2;
        const int MaxNameLength = 50;
        const int MaxDescriptionLength = 500;
        const int MaxRating = 5;
        private readonly ISharedValidator _sharedValidator;

        public CompanyValidator(ISharedValidator sharedValidator)
        {
            _sharedValidator = sharedValidator;
        }

        public bool Validate(CompanyInDto company)
        {
            if (_sharedValidator.TextGoodLength(company.Name, MaxNameLength, MinNameLength) &&
                _sharedValidator.TextGoodLength(company.Description, MaxDescriptionLength) &&
                _sharedValidator.TextGoodLength(company.Rating.Description, MaxDescriptionLength) &&
                RatingValid(company.Rating.AnimalsRating) && 
                RatingValid(company.Rating.PeopleRating) &&
                RatingValid(company.Rating.PlanetRating))
                return true;

            return false;
        }

        private bool RatingValid(int rating)
        {
            return rating >= 1 && rating <= MaxRating;
        }
    }
}
