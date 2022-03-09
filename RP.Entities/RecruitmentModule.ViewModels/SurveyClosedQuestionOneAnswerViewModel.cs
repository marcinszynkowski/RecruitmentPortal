using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.RecruitmentModule.ViewModels
{
    public class SurveyClosedQuestionOneAnswerViewModel
    {
        public int SurveyQuestionId { get; set; }

        public string Question { get; set; }

        public List<AnswerViewModel> Answers { get; set; }

        [Required(ErrorMessage ="*")]
        public int SelectedAnswer { get; set; }
    }
}