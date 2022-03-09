using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddRecruitmentProcessViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Kod procesu")]
        public int ProcessCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Nazwa firmy")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Miasto")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Stanowisko")]
        public int PositionId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Twoje zadania")]
        [StringLength(500, ErrorMessage = "Zadania nie mogą mieć więcej niż {1} liter.")]
        public string Tasks { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Czego wymagamy?")]
        [StringLength(500, ErrorMessage = "Wymagania nie mogą mieć więcej niż {1} liter.")]
        public string Requirements { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Co oferujemy?")]
        [StringLength(500, ErrorMessage = "Oferta nie może mieć więcej niż {1} liter.")]
        public string Offer { get; set; }

        [Display(Name = "Forma zatrudnenia")]
        public int? FormOfEmploymentId { get; set; }

        [Display(Name = "Rodzaj wynagrodzenia")]
        public int? PaymentTypeId { get; set; }

        [Display(Name = "Wynagrodzenie")]
        public int? Payment { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [StringLength(60, ErrorMessage = "Długość adresu email nie może przekraczać {0} znaków.")]
        [EmailAddress(ErrorMessage = "Proszę wprowadzić poprawny adres konta email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Telefon kontaktowy")]
        [StringLength(20, ErrorMessage = "Numer telefonu nie może przekraczać {0} cyfr.")]
        public string Phone { get; set; }
    }
}