using Business.Dto.InputDto;
using Business.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class Validator : IValidator
    {
        private readonly ICategoryProvider _categoryProvider;

        public Validator(ICategoryProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }

        public bool NumberWhole(int num)
        {
            return num >= 0;
        }

        public async Task<bool> BrandParamsValid(BrandParametersDto brandParams)
        {
            if (QueryValid(brandParams.Query) && SortTypeValid(brandParams.SortType) &&
                NumberWhole(brandParams.PageNumber) && NumberWhole(brandParams.PageSize) &&
                await CategoryIdValid(brandParams.CategoryId))
                return true;

            return false;
        }

        private bool QueryValid(string query)
        {
            if (query == null)
                return true;

            return query.Length <= 30;
        }

        private bool SortTypeValid(string sortType)
        {
            if (sortType == null)
                return true;

            var supportedSorts = new List<string>() { 
                "any", "total", "planet", "people", "animals"
            };

            return supportedSorts.Contains(sortType.ToLower());
        }

        private async Task<bool> CategoryIdValid(int categoryId)
        {
            return (categoryId == 0 || await _categoryProvider.KeyExists(categoryId));
        }
    }
}