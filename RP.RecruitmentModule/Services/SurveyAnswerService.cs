using RP.Data.Context;
using RP.Entities.Recruitment;
using RP.RecruitmentModule.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RP.RecruitmentModule.Services
{
    public class SurveyAnswerService : ISurveyAnswerService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public SurveyAnswerService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public SurveyAnswer GetSurveyAnswerById(int answerId)
        {
            return _db.SurveyAnswers.Find(answerId);
        }

        public string GetSurveyAnswer(int surveyAnswerId)
        {
            return GetSurveyAnswerById(surveyAnswerId).Answer;
        }

        public List<SurveyAnswer> GetSurveyAnswersByQuestion(int surveyQuestionId)
        {
            return _db.SurveyAnswers.Where(sa => sa.SurveyQuestionId == surveyQuestionId).ToList();
        }

        public List<SurveyAnswer> GetSurveyAnswersByUser(int userId)
        {
            return _db.SurveyAnswers.Where(sq => sq.UserId == userId).ToList();
        }
        #endregion

        #region Create
        public void CreateSurveyAnswer(int userId, int questionId, string answer)
        {
            SurveyAnswer surveyAnswer = new SurveyAnswer
            {
                UserId = userId,
                SurveyQuestionId = questionId,
                Answer = answer,
            };
            _db.SurveyAnswers.Add(surveyAnswer);
            _db.SaveChanges();
        }
        #endregion

        #region Delete
        public void DeleteSurveyQuestion(int userId, int questionId)
        {
            SurveyAnswer surveyAnswer = _db.SurveyAnswers.Where(sa => (sa.UserId == userId && sa.SurveyQuestionId == questionId)).FirstOrDefault();

            if (surveyAnswer != null)
            {
                _db.SurveyAnswers.Remove(surveyAnswer);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}