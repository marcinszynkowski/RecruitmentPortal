using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AccountModule.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Proszę wprowadzić poprawny adres konta email")]
        public string Email { get; set; }
    }
}