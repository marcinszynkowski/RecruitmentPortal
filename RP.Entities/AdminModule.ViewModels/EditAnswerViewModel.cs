using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class EditAnswerViewModel
    {
        public int AnswerId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Odpowiedź")]
        public string Answer { get; set; }

        public string SurveyQuestionName { get; set; }

        public int RecruitmentProcessId { get; set; }

        public int SurveyQuestionId { get; set; }

        public int SurveyQuestionTypeId { get; set; }
    }
}