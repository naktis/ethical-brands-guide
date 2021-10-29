using Business.Dto.InputDto;

namespace Business.Services.Interfaces
{
    public interface IDefaultSetter
    {
        public BrandParametersDto SetMissingBrandParams(BrandParametersDto brandParamsRaw);
    }
}
