using RP.Entities.Recruitment;
using System.Collections.Generic;

namespace RP.RecruitmentModule.Interfaces
{
    public interface ISurveyQuestionService
    {
        void CreateSurveyQuestion(int processId, string question, int questionTypeId);
        void CreateAnswer(int processId, int questionId, int questionTypeId, string answer);
        void DeleteSurveyQuestion(int surveyQuestionId);
        void DeleteAnswer(int answerId);
        SurveyQuestion GetSurveyQuestionById(int surveyQuestionId);
        List<SurveyQuestion> GetSurveyQuestionsForRecruitmentProcess(int processId);
        List<SurveyQuestion> GetAllAnswers(int questionId);
        void SetSurveyQuestion(int surveyQuestionId, string question);
    }
}