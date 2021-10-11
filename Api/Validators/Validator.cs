using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class Validator : IValidator
    {
        public bool KeyNegative(int key)
        {
            return key < 0;
        }

        public bool QueryTooLong(string query)
        {
            if (query == null)
                return false;

            return query.Length > 30;
        }

        public bool SortTypeInvalid(string sortType)
        {
            var supportedSorts = new List<string>() { 
                "any", "planet", "people", "animals"
            };

            return !supportedSorts.Contains(sortType);
        }
    }
}