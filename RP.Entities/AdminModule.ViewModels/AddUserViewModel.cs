using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [StringLength(60, ErrorMessage = "Długość adresu email nie może przekraczać {0} znaków.")]
        [EmailAddress(ErrorMessage = "Proszę wprowadzić poprawny adres konta email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Hasło")]
        [StringLength(30, ErrorMessage = "Hasło musi zawierać {2}-{1} znaków.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Hasło musi zawierać conajmniej po jednej dużej i małej literze oraz conajmniej jedną cyfrę.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Powtórz Hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne")]
        [StringLength(30, ErrorMessage = "Hasło musi zawierać {2}-{1} znaków.", MinimumLength = 6)]
        [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Hasło musi zawierać conajmniej po jednej dużej i małej literze oraz conajmniej jedną cyfrę.")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Imię")]
        [StringLength(25, ErrorMessage = "Imię nie może mieć więcej niż {1} liter.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Nazwisko")]
        [StringLength(50, ErrorMessage = "Nazwisko nie może mieć więcej niż {1} liter.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Rola")]
        public int RoleId { get; set; }
    }
}