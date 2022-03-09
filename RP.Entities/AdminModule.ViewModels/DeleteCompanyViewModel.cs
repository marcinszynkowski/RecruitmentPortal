using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteCompanyViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Firma")]
        public string Name { get; set; }
    }
}