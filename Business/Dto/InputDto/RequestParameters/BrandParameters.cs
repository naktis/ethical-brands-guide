namespace Business.Dto.InputDto.RequestParameters
{
    public class BrandParameters : PagingParameters
    {
        public string Query { get; set; }
        public string SortType { get; set; } = "any";
        public int CategoryId { get; set; }
    }
}
