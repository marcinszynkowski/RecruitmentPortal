using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteForeignLanguageViewModel
    {
        [Display(Name = "Język obcy")]
        public string Name { get; set; }
    }
}