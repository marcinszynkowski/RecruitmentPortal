using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class EditCourseTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Typ kursu")]
        public string Name { get; set; }
    }
}