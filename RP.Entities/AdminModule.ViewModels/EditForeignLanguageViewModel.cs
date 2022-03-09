using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class EditForeignLanguageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Język obcy")]
        public string Name { get; set; }
    }
}