using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteFormOfEmploymentViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Forma zatrudnienia")]
        public string Name { get; set; }
    }
}
