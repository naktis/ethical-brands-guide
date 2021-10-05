using System.Collections.Generic;

namespace Data.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }


        public ICollection<BrandCategory> BrandsCategories { get; set; }
    }
}
