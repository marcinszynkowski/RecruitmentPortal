using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteSurveyQuestionViewModel
    {
        public int SurveyQuestionId { get; set; }

        [Display(Name = "Pytanie")]
        public string Question { get; set; }

        public int RecruitmentProcessId { get; set; }

        public int RecruitmentProcessCode { get; set; }
    }
}