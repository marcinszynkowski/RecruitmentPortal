using System.Collections.Generic;

namespace RP.Entities.RecruitmentModule.ViewModels
{
    public class SurveyClosedQuestionMultipleAnswersViewModel
    {
        public int SurveyQuestionId { get; set; }

        public string Question { get; set; }

        public List<AnswerViewModel> Answers { get; set; }
    }
}