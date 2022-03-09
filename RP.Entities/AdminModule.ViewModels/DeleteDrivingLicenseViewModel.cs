using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteDrivingLicenseViewModel
    {
        [Display(Name = "Kategoria")]
        public string Category { get; set; }
    }
}