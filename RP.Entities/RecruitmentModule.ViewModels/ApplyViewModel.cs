using System.Collections.Generic;

namespace RP.Entities.RecruitmentModule.ViewModels
{
    public class ApplyViewModel
    {
        public int RecruitmentProcessId { get; set; }

        public int UserId { get; set; }

        public ApplyFilesViewModel ApplyFilesViewModel { get; set; }

        public IList<SurveyOpenQuestionViewModel> SurveyOpenQuestionViewModelList { get; set; }

        public IList<SurveyClosedQuestionOneAnswerViewModel> SurveyClosedQuestionOneAnswerViewModelList { get; set; }

        public IList<SurveyClosedQuestionMultipleAnswersViewModel> SurveyClosedQuestionMultipleAnswersModelList { get; set; }
    }
}