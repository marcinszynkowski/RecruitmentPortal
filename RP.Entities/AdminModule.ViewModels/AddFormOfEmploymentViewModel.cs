using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddFormOfEmploymentViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Forma zatrudnienia")]
        public string Name { get; set; }
    }
}
