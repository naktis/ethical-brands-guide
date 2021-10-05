using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public ICollection<BrandCategory> BrandsCategories { get; set; }
    }
}
