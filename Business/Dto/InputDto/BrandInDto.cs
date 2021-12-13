using System.Collections.Generic;

namespace Business.Dto.InputDto
{
    public class BrandInDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
        public int? RequestId { get; set; }
    }
}
