using Business.Dto.InputDto;
using Business.Services.Interfaces;

namespace Business.Services
{
    public class DefaultSetter : IDefaultSetter
    {
        const int MaxPageSize = 50;
        const int DefaultPageSize = 10;

        public BrandParametersDto SetMissingBrandParams(BrandParametersDto brandParams)
        {
            if (brandParams.SortType == null)
                brandParams.SortType = "any";

            if (brandParams.PageSize == 0)
                brandParams.PageSize = DefaultPageSize;

            if (brandParams.PageSize > MaxPageSize)
                brandParams.PageSize = MaxPageSize;

            if (brandParams.PageNumber == 0)
                brandParams.PageNumber = 1;

            if (brandParams.PageNumber == 0)
                brandParams.PageNumber = 1;

            return brandParams;
        }
    }
}
