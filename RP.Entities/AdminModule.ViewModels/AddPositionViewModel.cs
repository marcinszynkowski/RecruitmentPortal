using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddPositionViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Stanowisko")]
        public string Name { get; set; }
    }
}