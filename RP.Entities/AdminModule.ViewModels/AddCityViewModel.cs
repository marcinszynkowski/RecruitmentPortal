using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddCityViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Miasto")]
        public string Name { get; set; }
    }
}