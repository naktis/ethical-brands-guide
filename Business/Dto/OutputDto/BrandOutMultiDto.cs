namespace Business.Dto.OutputDto
{
    public class BrandOutMultiDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string CompanyName { get; set; }
        public double RatingTotal { get; set; }
        public int RatingAnimals { get; set; }
        public int RatingPeople { get; set; }
        public int RatingPlanet { get; set; }
    }
}
