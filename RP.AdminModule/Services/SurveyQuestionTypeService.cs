using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Recruitment;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class SurveyQuestionTypeService : ISurveyQuestionTypeService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public SurveyQuestionTypeService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public SurveyQuestionType GetSurveyQuestionTypeByName(string surveyQuestionTypeName)
        {
            return _db.SurveyQuestionTypes.Where(ck => ck.Name == surveyQuestionTypeName).FirstOrDefault();
        }

        public SurveyQuestionType GetSurveyQuestionTypeById(int surveyQuestionTypeId)
        {
            return _db.SurveyQuestionTypes.Find(surveyQuestionTypeId);
        }

        public string GetSurveyQuestionTypeName(int surveyQuestionTypeId)
        {
            return _db.SurveyQuestionTypes.Find(surveyQuestionTypeId).Name;
        }

        public List<SurveyQuestionType> GetAllSurveyQuestionTypes()
        {
            return _db.SurveyQuestionTypes.ToList();
        }
        #endregion

        #region Set
        public void SetSurveyQuestionTypeName(int surveyQuestionTypeId, string surveyQuestionTypeName)
        {
            SurveyQuestionType surveyQuestionType = GetSurveyQuestionTypeById(surveyQuestionTypeId);
            surveyQuestionType.Name = surveyQuestionTypeName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateSurveyQuestionType(string surveyQuestionTypeName)
        {
            if (GetSurveyQuestionTypeByName(surveyQuestionTypeName) == null)
            {
                SurveyQuestionType surveyQuestionType = new SurveyQuestionType
                {
                    Name = surveyQuestionTypeName,
                };
                _db.SurveyQuestionTypes.Add(surveyQuestionType);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteSurveyQuestionType(string surveyQuestionTypeName)
        {
            SurveyQuestionType surveyQuestionType = GetSurveyQuestionTypeByName(surveyQuestionTypeName);

            if (surveyQuestionType != null)
            {
                _db.SurveyQuestionTypes.Remove(surveyQuestionType);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}