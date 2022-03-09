using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddForeignLanguageLevelViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Poziom języka obcego")]
        public string Name { get; set; }
    }
}