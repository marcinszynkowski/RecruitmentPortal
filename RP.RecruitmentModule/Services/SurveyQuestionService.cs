using RP.Data.Context;
using RP.Entities.Recruitment;
using RP.RecruitmentModule.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RP.RecruitmentModule.Services
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public SurveyQuestionService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public SurveyQuestion GetSurveyQuestionById(int surveyQuestionId)
        {
            return _db.SurveyQuestions.Find(surveyQuestionId);
        }

        public List<SurveyQuestion> GetSurveyQuestionsForRecruitmentProcess(int processId)
        {
            return _db.SurveyQuestions.Where(sq => sq.RecruitmentProcessId == processId).ToList();
        }

        public List<SurveyQuestion> GetAllAnswers(int questionId)
        {
            return _db.SurveyQuestions.Where(sq => sq.ParentQuestionId == questionId).ToList();
        }
        #endregion

        #region Set
        public void SetSurveyQuestion(int surveyQuestionId, string question)
        {
            SurveyQuestion surveyQuestion = GetSurveyQuestionById(surveyQuestionId);
            surveyQuestion.Question = question;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateSurveyQuestion(int processId, string question, int questionTypeId)
        {
            SurveyQuestion surveyQuestion = new SurveyQuestion
            {
                RecruitmentProcessId = processId,
                Question = question,
                SurveyQuestionTypeId = questionTypeId
            };
            _db.SurveyQuestions.Add(surveyQuestion);
            _db.SaveChanges();
        }

        public void CreateAnswer(int processId, int questionId, int questionTypeId, string answer)
        {
            SurveyQuestion surveyQuestion = new SurveyQuestion
            {
                RecruitmentProcessId = processId,
                Question = answer,
                SurveyQuestionTypeId = questionTypeId,
                ParentQuestionId = questionId
            };
            _db.SurveyQuestions.Add(surveyQuestion);
            _db.SaveChanges();
        }
        #endregion

        #region Delete
        public void DeleteSurveyQuestion(int surveyQuestionId)
        {
            SurveyQuestion surveyQuestion = GetSurveyQuestionById(surveyQuestionId);

            List<SurveyQuestion> surveyQuestionsList = GetAllAnswers(surveyQuestionId);

            if (surveyQuestion != null)
            {
                foreach (var question in surveyQuestionsList)
                {
                    _db.SurveyQuestions.Remove(question);
                }
                _db.SurveyQuestions.Remove(surveyQuestion);
                _db.SaveChanges();
            }
        }

        public void DeleteAnswer(int answerId)
        {
            SurveyQuestion surveyQuestion = GetSurveyQuestionById(answerId);

            if (surveyQuestion != null)
            {
                _db.SurveyQuestions.Remove(surveyQuestion);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}