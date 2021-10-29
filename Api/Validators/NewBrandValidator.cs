using Api.Validators.Interfaces;
using Business.Dto.InputDto;
using Business.Services.Interfaces;

namespace Api.Validators
{
    public class NewBrandValidator : INewBrandValidator
    {
        const int MinNameLength = 2;
        const int MaxNameLength = 50;
        const int MaxDescriptionLength = 500;
        private readonly ICompanyProvider _companyProvider;
        private readonly IBrandProvider _brandProvider;

        public NewBrandValidator(ICompanyProvider companyProvider, IBrandProvider brandProvider)
        {
            _companyProvider = companyProvider;
            _brandProvider = brandProvider;
        }
        public bool Validate(BrandInDto brand)
        {
            if (TextGoodLength(brand.Name, MaxNameLength, MinNameLength) &&
                TextGoodLength(brand.Description, MaxDescriptionLength) &&
                _companyProvider.KeyExists(brand.CompanyId).Result &&
                !_brandProvider.Exists(brand).Result)
                return true;
            return false;
        }

        private bool TextGoodLength(string text, int max, int min = 0)
        {
            return text.Length >= min && text.Length <= max;
        }
    }
}
