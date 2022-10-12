using System.ComponentModel.DataAnnotations;

namespace VL.Shared.Model
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? Password { get; set; } = string.Empty;

    }

    public class UserDTO : LoginDTO
    {
        public string Role { get; set; } = string.Empty;
    }
}
