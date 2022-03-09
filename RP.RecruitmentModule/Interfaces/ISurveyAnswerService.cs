using RP.Entities.Recruitment;
using System.Collections.Generic;

namespace RP.RecruitmentModule.Interfaces
{
    public interface ISurveyAnswerService
    {
        void CreateSurveyAnswer(int userId, int questionId, string answer);
        void DeleteSurveyQuestion(int userId, int questionId);
        string GetSurveyAnswer(int surveyAnswerId);
        SurveyAnswer GetSurveyAnswerById(int answerId);
        List<SurveyAnswer> GetSurveyAnswersByQuestion(int surveyQuestionId);
        List<SurveyAnswer> GetSurveyAnswersByUser(int userId);
    }
}