namespace Data.Models
{
    public class Request
    {
        public int RequestId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        public int? BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
