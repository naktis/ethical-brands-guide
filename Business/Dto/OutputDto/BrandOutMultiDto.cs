namespace Business.Dto.OutputDto
{
    public class BrandOutMultiDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string CompanyName { get; set; }
        public double RatingTotal { get; set; }
        public double RatingAnimals { get; set; }
        public double RatingPeople { get; set; }
        public double RatingPlanet { get; set; }
    }
}
