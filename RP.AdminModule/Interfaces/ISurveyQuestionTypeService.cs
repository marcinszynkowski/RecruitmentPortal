using System.Collections.Generic;
using RP.Entities.Recruitment;

namespace RP.AdminModule.Interfaces
{
    public interface ISurveyQuestionTypeService
    {
        void CreateSurveyQuestionType(string surveyQuestionTypeName);
        void DeleteSurveyQuestionType(string surveyQuestionTypeName);
        List<SurveyQuestionType> GetAllSurveyQuestionTypes();
        SurveyQuestionType GetSurveyQuestionTypeById(int surveyQuestionTypeId);
        SurveyQuestionType GetSurveyQuestionTypeByName(string surveyQuestionTypeName);
        string GetSurveyQuestionTypeName(int surveyQuestionTypeId);
        void SetSurveyQuestionTypeName(int surveyQuestionTypeId, string surveyQuestionTypeName);
    }
}