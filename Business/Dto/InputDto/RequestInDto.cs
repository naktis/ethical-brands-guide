using Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Business.Dto.InputDto
{
    public class RequestInDto
    {
        [Required(ErrorMessage = "Brand name is required")]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        private string email { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email 
        { 
            get { return email; } 
            set { email = value == "" ? null : value; } 
        }

        [MaxLength(500)]
        public string Description { get; set; }

        public Request GetEntity()
        {
            return new Request
            {
                Name = Name,
                Email = Email,
                Description = Description
            };
        }
    }
}
