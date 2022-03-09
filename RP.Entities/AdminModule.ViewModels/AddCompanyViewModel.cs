using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddCompanyViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Firma")]
        public string Name { get; set; }
    }
}