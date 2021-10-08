using System.Collections.Generic;

namespace Business.Dto.OutputDto
{
    public class BrandOutDto
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CompanyOutDto Company { get; set; }
        public ICollection<CategoryOutDto> Categories { get; set; }
    }
}
