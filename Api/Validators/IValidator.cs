using Business.Dto.InputDto;
using System.Threading.Tasks;

namespace Api.Validators
{
    public interface IValidator
    {
        public bool NumberWhole(int key);
        public Task<bool> BrandParamsValid(BrandParametersDto brandParams);
    }
}
