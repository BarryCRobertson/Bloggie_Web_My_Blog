using System.ComponentModel.DataAnnotations;

namespace Bloggie.Webb.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }
       [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(8, ErrorMessage ="The password length must be at least 8 characters")]
        public string Password { get; set; }
       
        
    }
}
