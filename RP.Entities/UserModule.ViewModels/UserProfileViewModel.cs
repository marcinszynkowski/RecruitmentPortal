using System;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.UserModule.ViewModels
{
    public class UserProfileViewModel
    {
        [Key]
        public Account.User User{ get; set; }
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [StringLength(60, ErrorMessage = "Długość adresu email nie może przekraczać {0} znaków.")]
        [EmailAddress(ErrorMessage = "Proszę wprowadzić poprawny adres konta email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Imię")]
        [StringLength(25, ErrorMessage = "Imię nie może mieć więcej niż {0} liter.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Nazwisko")]
        [StringLength(50, ErrorMessage = "Nazwisko nie może mieć więcej niż {0} liter.")]
        public string LastName { get; set; }

        [Display(Name = "Data Urodzenia")]
        public DateTime? Birthday { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Display(Name = "Telefon")]
        public string Phone { get; set; }
    }
}
