using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class RegisterViewModel
    {
        [Required] 
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required] 
        public string FullName { get; set; }
        [Required] 
        public string PhoneNumber { get; set; }
        [Required] 
        public string Email { get; set; }
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
