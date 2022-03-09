using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class AddAnswerViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Odpowiedź")]
        public string Answer { get; set; }

        public int QuestionId { get; set; }

        public string SurveyQuestionName { get; set; }

        public int RecruitmentProcessId { get; set; }

        public int SurveyQuestionTypeId { get; set; }
    }
}