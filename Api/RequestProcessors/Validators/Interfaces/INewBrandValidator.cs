using Business.Dto.InputDto;

namespace Api.RequestProcessors.Validators.Interfaces
{
    public interface INewBrandValidator : IValidator<BrandInDto> 
    {
        public bool Validate(BrandInDto dto, int key);
    }
}
