using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddCourseTypeViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Typ kursu")]
        public string Name { get; set; }
    }
}