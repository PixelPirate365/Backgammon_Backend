using System.ComponentModel.DataAnnotations;

namespace Auth.Server.Quickstart.ViewModels {
    public class ResetPasswordModel {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirm password doesnot match")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
