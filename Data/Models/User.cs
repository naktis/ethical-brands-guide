using System.Collections.Generic;

namespace Data.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserTypes Type { get; set; }

        public ICollection<Brand> Brands { get; set; }
    }
}
