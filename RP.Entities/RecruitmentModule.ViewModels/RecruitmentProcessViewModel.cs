using System.ComponentModel.DataAnnotations;

namespace RP.Entities.RecruitmentModule.ViewModels
{
    public class RecruitmentProcessViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Kod procesu")]
        public int ProcessCode { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Firma")]
        public string Company { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Stanowisko")]
        public string Position { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Twoje zadania")]
        public string Tasks { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Czego wymagamy?")]
        public string Requirements { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Co oferujemy?")]
        public string Offer { get; set; }

        [Display(Name = "Forma zatrudnienia")]
        public string FormOfEmployment { get; set; }

        [Display(Name = "Rodzaj wynagrodzenia")]
        public string PaymentType { get; set; }

        [Display(Name = "Stawka")]
        public int? Payment { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }
    }
}
