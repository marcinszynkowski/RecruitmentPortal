using System.ComponentModel.DataAnnotations;

namespace RP.Entities.RecruitmentModule.ViewModels
{
    public class SurveyOpenQuestionViewModel
    {
        public int SurveyQuestionId { get; set; }

        public string Question { get; set; }

        [Required(ErrorMessage = "*")]
        public string Answer { get; set; }
    }
}