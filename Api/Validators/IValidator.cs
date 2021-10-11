using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public interface IValidator
    {
        public bool KeyNegative(int key);
        public bool QueryTooLong(string query);
        public bool SortTypeInvalid(string sortType);
    }
}
