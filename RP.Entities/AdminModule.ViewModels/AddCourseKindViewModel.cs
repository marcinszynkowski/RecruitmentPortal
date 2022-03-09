using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddCourseKindViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Rodzaj kursu")]
        public string Name { get; set; }
    }
}