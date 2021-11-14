using Api.RequestProcessors.Validators.Interfaces;
using Business.Dto.InputDto;
using Business.Services.Interfaces;

namespace Api.RequestProcessors.Validators
{
    public class NewBrandValidator : INewBrandValidator
    {
        const int MinNameLength = 2;
        const int MaxNameLength = 50;
        const int MaxDescriptionLength = 500;
        private readonly ICompanyProvider _companyProvider;
        private readonly IBrandProvider _brandProvider;
        private readonly ISharedValidator _sharedValidator;

        public NewBrandValidator(ICompanyProvider companyProvider, IBrandProvider brandProvider,
            ISharedValidator sharedValidator)
        {
            _companyProvider = companyProvider;
            _brandProvider = brandProvider;
            _sharedValidator = sharedValidator;
        }
        public bool Validate(BrandInDto brand)
        {
            if (_sharedValidator.TextGoodLength(brand.Name, MaxNameLength, MinNameLength) &&
                _sharedValidator.TextGoodLength(brand.Description, MaxDescriptionLength) &&
                _companyProvider.KeyExists(brand.CompanyId).Result &&
                !_brandProvider.Exists(brand).Result)
                return true;
            return false;
        }
    }
}
