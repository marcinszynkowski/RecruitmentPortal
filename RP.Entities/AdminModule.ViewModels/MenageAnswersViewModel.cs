using RP.Entities.Recruitment;
using System.Collections.Generic;

namespace RP.Entities.AdminModule.ViewModels
{
    public class MenageAnswersViewModel
    {
        public List<SurveyQuestion> SurveyQuestionList { get; set; }

        public int RecruitmentProcessId { get; set; }

        public int SurveyQuestionId { get; set; }

        public int SurveyQuestionTypeId { get; set; }
    }
}