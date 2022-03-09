using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteCityViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Miasto")]
        public string Name { get; set; }
    }
}