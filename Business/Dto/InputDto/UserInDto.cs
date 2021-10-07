using Commons;

namespace Business.Dto.InputDto
{
    public class UserInDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public UserType Type { get; set; }
    }
}
