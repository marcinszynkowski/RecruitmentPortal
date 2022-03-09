using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class EditSurveyQuestionViewModel
    {
        public int SurveyQuestionId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Pytanie")]
        public string Question { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Typ pytania")]
        public int QuestionTypeId { get; set; }

        public int RecruitmentProcessId { get; set; }

        public int RecruitmentProcessCode { get; set; }
    }
}