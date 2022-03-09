using System.ComponentModel.DataAnnotations;

namespace RP.Entities.UserModule.ViewModels
{
    public class DeleteWorkExperienceViewModel
    {
        [Display(Name = "Stanowisko")]
        public string Position{ get; set; }
    }
}
