using System.ComponentModel.DataAnnotations;
using System.Web;

namespace RP.Entities.RecruitmentModule.ViewModels
{
    public class ApplyFilesViewModel
    {
        [Display(Name = "CV")]
        public HttpPostedFileBase CV { get; set; }

        [Display(Name = "List motywacyjny")]
        public HttpPostedFileBase MotivationLetter { get; set; }

        public bool SaveCVToDatabase { get; set; }

        public bool SaveMotivationLetterToDatabase { get; set; }
    }
}