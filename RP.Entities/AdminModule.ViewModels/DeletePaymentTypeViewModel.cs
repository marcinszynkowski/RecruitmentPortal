using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeletePaymentTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Rodzaj wynagrodzenia")]
        public string Name { get; set; }
    }
}
