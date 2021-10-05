using System.Collections.Generic;

namespace Data.Models
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public int RatingId { get; set; }
        public Rating Rating { get; set; }

        public ICollection<Brand> Brands { get; set; }
    }
}
