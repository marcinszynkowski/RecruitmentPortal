using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteRecruitmentProcessViewModel
    {
        [Display(Name = "Kod procesu")]
        public int ProcessCode { get; set; }
    }
}
