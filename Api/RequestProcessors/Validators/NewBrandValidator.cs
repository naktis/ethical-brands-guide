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
        private readonly IRequestProvider _requestProvider;

        public NewBrandValidator(ICompanyProvider companyProvider, IBrandProvider brandProvider,
            ISharedValidator sharedValidator, IRequestProvider requestProvider)
        {
            _companyProvider = companyProvider;
            _brandProvider = brandProvider;
            _sharedValidator = sharedValidator;
            _requestProvider = requestProvider;
        }

        public bool Validate(BrandInDto brand)
        {
            var requestId = brand.RequestId ?? default;

            return ValidateBrandFields(brand) &&
                !_brandProvider.Exists(brand).Result &&
                (requestId == default || _requestProvider.KeyExists(requestId).Result);
        }

        public bool Validate(BrandInDto brand, int key)
        {
            return ValidateBrandFields(brand) && 
                _brandProvider.NameAvailable(brand, key).Result;
        }

        private bool ValidateBrandFields(BrandInDto brand)
        {
            return _sharedValidator.TextGoodLength(brand.Name, MaxNameLength, MinNameLength) &&
                _sharedValidator.TextGoodLength(brand.Description, MaxDescriptionLength) &&
                _companyProvider.KeyExists(brand.CompanyId).Result;
        }
    }
}
