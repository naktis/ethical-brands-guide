namespace Business.Dto.InputDto
{
    public class BrandParametersDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public string SortType { get; set; }
        public int CategoryId { get; set; }
    }
}
