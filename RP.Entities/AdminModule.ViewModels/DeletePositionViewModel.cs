using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeletePositionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Stanowisko")]
        public string Name { get; set; }
    }
}