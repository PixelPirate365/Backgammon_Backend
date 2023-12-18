using System.ComponentModel.DataAnnotations;

namespace Auth.Server.Quickstart.ViewModels {
    public class ForgotPasswordModel {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
