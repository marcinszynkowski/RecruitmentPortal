using RP.Entities.Recruitment;
using System.Collections.Generic;

namespace RP.Entities.AdminModule.ViewModels
{
    public class MenageSurveyQuestionsViewModel
    {
        public List<SurveyQuestion> SurveyQuestionList { get; set; }

        public int RecruitmentProcessId { get; set; }

        public int RecruitmentProcessCode { get; set; }
    }
}