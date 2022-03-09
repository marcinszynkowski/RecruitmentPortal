using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteCourseTypeViewModel
    {
        [Display(Name = "Typ kursu")]
        public string Name { get; set; }
    }
}