using Api.Validators.Interfaces;
using Business.Dto.InputDto;
using Business.Services.Interfaces;
using System.Collections.Generic;

namespace Api.Validators
{
    public class BrandParamsValidator : IBrandParamsValidator
    {
        private readonly ICategoryProvider _categoryProvider;

        public BrandParamsValidator(ICategoryProvider categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }

        public bool Validate(BrandParametersDto brandParams)
        {
            if (QueryValid(brandParams.Query) &&
                SortTypeValid(brandParams.SortType) &&
                NumberWhole(brandParams.PageNumber) &&
                NumberWhole(brandParams.PageSize) &&
                CategoryIdValid(brandParams.CategoryId))
                return true;

            return false;
        }

        private bool NumberWhole(int num)
        {
            return num >= 0;
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

        private bool CategoryIdValid(int categoryId)
        {
            return (categoryId == 0 || _categoryProvider.KeyExists(categoryId).Result);
        }
    }
}
