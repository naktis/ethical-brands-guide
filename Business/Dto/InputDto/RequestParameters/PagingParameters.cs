using System.ComponentModel.DataAnnotations;

namespace Business.Dto.InputDto.RequestParameters
{
    public class PagingParameters
    {
        private int pageNumber = 1;

        public int PageNumber
        {
            get
            {
                return pageNumber;
            }
            set
            {
                if (value < 1)
                    pageNumber = 1;
                else pageNumber = value;
            }
        }

        private int pageSize = defaultPageSize;

        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                if (value > maxPageSize)
                    pageSize = maxPageSize;
                else if (value < 1)
                    pageSize = defaultPageSize;
                else pageSize = value;
            }
        }

        const int maxPageSize = 50;
        const int defaultPageSize = 10;
    }
}
