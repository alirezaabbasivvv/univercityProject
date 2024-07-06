using System.ComponentModel.DataAnnotations;

namespace univercityProject.Models.Dtos
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
