using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddForeignLanguageViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Język obcy")]
        public string Name { get; set; }
    }
}