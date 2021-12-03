using Data.Models;

namespace Business.Dto.OutputDto
{
    public class RequestOutDto
    {
        public RequestOutDto(Request request)
        {
            RequestId = request.RequestId;
            Name = request.Name;
            Email = request.Email;
            Description = request.Description;
        }

        public int RequestId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
    }
}
