using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddDrivingLicenseViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
    }
}