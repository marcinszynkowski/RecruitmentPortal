using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddPaymentTypeViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Rodzaj wynagrodzenia")]
        public string Name { get; set; }
    }
}
