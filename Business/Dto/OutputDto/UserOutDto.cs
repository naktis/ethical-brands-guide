using Data.Models;

namespace Business.Dto.OutputDto
{
    public class UserOutDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
    }
}
