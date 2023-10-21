using System.ComponentModel.DataAnnotations;

namespace ManagingLib.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalide Email Address")]
        public string Email { get; set; }
    }
}
