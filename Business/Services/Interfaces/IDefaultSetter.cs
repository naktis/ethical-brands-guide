using Business.Dto.InputDto;
using Data.Models;

namespace Business.Services.Interfaces
{
    public interface IDefaultSetter
    {
        public BrandParametersDto SetMissingBrandParams(BrandParametersDto brandParamsRaw);
        public User SetDefaultUserType(User user);
    }
}
