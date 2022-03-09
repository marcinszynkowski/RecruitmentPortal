using RP.AccountModule.Interfaces;
using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Account;
using RP.Entities.Recruitment;
using RP.Entities.User;
using RP.RecruitmentModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace RP.AdminModule.Services
{
    public class XmlService : IXmlService
    {
        #region Fields
        private readonly RPDbContext _db;
        private readonly IRecruitmentProcessService _recruitmentProcessService;
        private readonly IUserService _userService;
        private readonly ISurveyAnswerService _surveyAnswerService;
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ISurveyQuestionTypeService _surveyQuestionTypeService;
        private readonly ICompanyService _companyService;
        private readonly ICityService _cityService;
        private readonly IPositionService _positionService;
        private readonly IFormOfEmploymentService _formOfEmploymentService;
        private readonly IPaymentTypeService _paymentTypeService;
        #endregion

        #region Constructors
        public XmlService(RPDbContext db, IRecruitmentProcessService recruitmentProcessService, IUserService userService, ISurveyAnswerService surveyAnswerService)
        {
            _db = db;
            _recruitmentProcessService = recruitmentProcessService;
            _userService = userService;
            _surveyAnswerService = surveyAnswerService;
        }

        public XmlService(RPDbContext db, IRecruitmentProcessService recruitmentProcessService, IUserService userService, ISurveyAnswerService surveyAnswerService,
            ISurveyQuestionService surveyQuestionService, ISurveyQuestionTypeService surveyQuestionTypeService, ICompanyService companyService, ICityService cityService,
            IPositionService positionService, IFormOfEmploymentService formOfEmploymentService, IPaymentTypeService paymentTypeService)
        {
            _db = db;
            _recruitmentProcessService = recruitmentProcessService;
            _userService = userService;
            _surveyAnswerService = surveyAnswerService;
            _surveyQuestionService = surveyQuestionService;
            _surveyQuestionTypeService = surveyQuestionTypeService;
            _companyService = companyService;
            _cityService = cityService;
            _positionService = positionService;
            _formOfEmploymentService = formOfEmploymentService;
            _paymentTypeService = paymentTypeService;
        }
        #endregion

        #region Import
        public void ImportRecruitmentProcesses()
        {
            string path = ConfigurationManager.AppSettings["xmlImport"];
            XDocument doc;
            try
            {
                doc = XDocument.Load(path);
            }
            catch (FileNotFoundException)
            {
                doc = new XDocument(new XElement("Processes"));
                doc.Save(path);
            }
            foreach (var process in doc.Descendants("Process"))
            {
                CreateRecruitmentProcessFromXml(process);
            }
        }

        private void CreateRecruitmentProcessFromXml(XElement process)
        {
            if (!IsProcessExists(Convert.ToInt32(process.Attribute("code").Value)))
            {
                using (DbContextTransaction transaction = _db.Database.BeginTransaction())
                {
                    try
                    {
                        int companyId = GetCompanyId(process.Element("Company").Value);
                        int cityId = GetCityId(process.Element("City").Value);
                        int positionId = GetPositionId(process.Element("Position").Value);

                        int formOfEmploymentId;
                        if (process.Element("FormOfEmployment") != null)
                        {
                            formOfEmploymentId = GetFormOfEmploymentId(process.Element("FormOfEmployment").Value);
                        }
                        else
                        {
                            formOfEmploymentId = _formOfEmploymentService.GetFormOfEmploymentByName("Nieokreślone").Id;
                        }

                        int paymentTypeId;
                        if (process.Element("PaymentType") != null)
                        {
                            paymentTypeId = GetPaymentTypeId(process.Element("PaymentType").Value);
                        }
                        else
                        {
                            paymentTypeId = _paymentTypeService.GetPaymentTypeByName("Nieokreślone").Id;
                        }

                        int? payment = null;
                        if (process.Element("Payment") != null)
                        {
                            payment = Convert.ToInt32(process.Element("Payment").Value);
                        }

                        _recruitmentProcessService.CreateRecruitmentProcess(Convert.ToInt32(process.Attribute("code").Value),
                        companyId, cityId, positionId, process.Element("Tasks").Value, process.Element("Requirements").Value,
                        process.Element("Offer").Value, formOfEmploymentId, paymentTypeId, payment, process.Element("Email").Value, process.Element("Phone").Value);

                        CreateSurveyQuestionsFromXml(process, Convert.ToInt32(process.Attribute("code").Value));

                        transaction.Commit();
                    }
                    catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
                    {
                        var entries = ex.Entries;
                        foreach (var entry in entries)
                        {
                            entry.State = EntityState.Detached;
                        }
                        transaction.Rollback();
                    }
                }
            }
        }

        private bool IsProcessExists(int code)
        {
            if (_recruitmentProcessService.GetRecruitmentProcessByProcessCode(code) != null)
            {
                return true;
            }
            return false;
        }

        private int GetCompanyId(string company)
        {
            int companyId;
            if (_companyService.GetCompanyByName(company) != null)
            {
                companyId = _companyService.GetCompanyByName(company).Id;
            }
            else
            {
                _companyService.CreateCompany(company);
                companyId = _companyService.GetCompanyByName(company).Id;
            }
            return companyId;
        }

        private int GetCityId(string city)
        {
            int cityId;
            if (_cityService.GetCityByName(city) != null)
            {
                cityId = _cityService.GetCityByName(city).Id;
            }
            else
            {
                _cityService.CreateCity(city);
                cityId = _cityService.GetCityByName(city).Id;
            }
            return cityId;
        }

        private int GetPositionId(string position)
        {
            int positionId;
            if (_positionService.GetPositionByName(position) != null)
            {
                positionId = _positionService.GetPositionByName(position).Id;
            }
            else
            {
                _positionService.CreatePosition(position);
                positionId = _positionService.GetPositionByName(position).Id;
            }
            return positionId;
        }

        private int GetFormOfEmploymentId(string formOfEmployment)
        {
            int formOfEmploymentId;
            if (_formOfEmploymentService.GetFormOfEmploymentByName(formOfEmployment) != null)
            {
                formOfEmploymentId = _formOfEmploymentService.GetFormOfEmploymentByName(formOfEmployment).Id;
            }
            else
            {
                _formOfEmploymentService.CreateFormOfEmployment(formOfEmployment);
                formOfEmploymentId = _formOfEmploymentService.GetFormOfEmploymentByName(formOfEmployment).Id;
            }
            return formOfEmploymentId;
        }

        private int GetPaymentTypeId(string paymentType)
        {
            int paymentTypeId;
            if (_paymentTypeService.GetPaymentTypeByName(paymentType) != null)
            {
                paymentTypeId = _paymentTypeService.GetPaymentTypeByName(paymentType).Id;
            }
            else
            {
                _paymentTypeService.CreatePaymentType(paymentType);
                paymentTypeId = _paymentTypeService.GetPaymentTypeByName(paymentType).Id;
            }
            return paymentTypeId;
        }

        private void CreateSurveyQuestionsFromXml(XElement process, int code)
        {
            RecruitmentProcess recruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessByProcessCode(code);

            foreach (var question in process.Element("Survey").Descendants("Question"))
            {
                int questionTypeId = GetQuestionTypeId(question.Attribute("type").Value);
                _surveyQuestionService.CreateSurveyQuestion(recruitmentProcess.Id, question.Attribute("question").Value, questionTypeId);
                if (question.Attribute("type").Value == "closedOne" || question.Attribute("type").Value == "closedMany")
                {
                    int questionId = _surveyQuestionService.GetSurveyQuestionsForRecruitmentProcess(recruitmentProcess.Id).LastOrDefault().Id;
                    foreach (var answer in question.Descendants())
                    {
                        _surveyQuestionService.CreateAnswer(recruitmentProcess.Id, questionId, questionTypeId, answer.Value);
                    }
                }
            }
        }

        private int GetQuestionTypeId(string questionType)
        {
            int questionTypeId;
            if (questionType == "open")
            {
                questionTypeId = _surveyQuestionTypeService.GetSurveyQuestionTypeByName("Otwarte").Id;
            }
            else if (questionType == "closedOne")
            {
                questionTypeId = _surveyQuestionTypeService.GetSurveyQuestionTypeByName("Zamknięte jednokrotnego wyboru").Id;
            }
            else
            {
                questionTypeId = _surveyQuestionTypeService.GetSurveyQuestionTypeByName("Zamknięte wielokrotnego wyboru").Id;
            }
            return questionTypeId;
        }
        #endregion

        #region Export
        public void ExportApplicantsData()
        {
            foreach (RecruitmentProcess recruitmentProcess in _recruitmentProcessService.GetAllRecruitmentProcesses())
            {
                foreach (User user in recruitmentProcess.Users)
                {
                    CreateApplicantsData(recruitmentProcess, user);
                }
            }
        }

        private void CreateApplicantsData(RecruitmentProcess recruitmentProcess, User user)
        {
            string path = ConfigurationManager.AppSettings["xmlExport"];
            XDocument doc;
            XElement processElement;
            XElement userElement;
            XElement element;
            PersonalData personalData = _userService.GetUserPersonalDataById(user.Id);

            try
            {
                doc = XDocument.Load(path);
            }
            catch (FileNotFoundException)
            {
                doc = new XDocument(new XElement("Processes"));
                doc.Save(path);
            }

            processElement = doc.Descendants("Process").Where(el => el.Attribute("Code").Value == recruitmentProcess.ProcessCode.ToString()).FirstOrDefault();
            if (processElement == null)
            {
                element = new XElement(
                    new XElement("Process", new XAttribute("Code", recruitmentProcess.ProcessCode.ToString()),
                        new XElement("User", new XAttribute("Id", user.Id.ToString()),
                            new XElement("FirstName", personalData.FirstName),
                            new XElement("LastName", personalData.LastName),
                            new XElement("Email", user.Email),
                            new XElement("Birthday", Convert.ToDateTime(personalData.Birthday).ToShortDateString()),
                            new XElement("City", personalData.City),
                            new XElement("Phone", personalData.Phone),
                            new XElement("VirtualCVAdress", user.VirtualCVAdress),
                            new XElement("Survey")
                            )
                        )
                    );

                CreateSurveyAnswers(user.Id, recruitmentProcess.Id, element);
                doc.Element("Processes").Add(element);
            }
            else
            {
                userElement = processElement.Descendants("User").Where(el => el.Attribute("Id").Value == user.Id.ToString()).FirstOrDefault();

                if (userElement == null)
                {
                    element = new XElement(
                        new XElement("User", new XAttribute("Id", user.Id.ToString()),
                            new XElement("FirstName", personalData.FirstName),
                            new XElement("LastName", personalData.LastName),
                            new XElement("Email", user.Email),
                            new XElement("Birthday", Convert.ToDateTime(personalData.Birthday).ToShortDateString()),
                            new XElement("City", personalData.City),
                            new XElement("Phone", personalData.Phone),
                            new XElement("VirtualCVAdress", user.VirtualCVAdress),
                            new XElement("Survey")
                            )
                        );
                    CreateSurveyAnswers(user.Id, recruitmentProcess.Id, element);
                    processElement.Add(element);
                }
            }
            doc.Save(path);
        }

        private void CreateSurveyAnswers(int userId, int recruitmentProcessId, XElement element)
        {
            XElement surveyElement;
            List<SurveyAnswer> surveyAnswerList = _surveyAnswerService.GetSurveyAnswersByUser(userId).Where(rp => rp.SurveyQuestions.RecruitmentProcessId == recruitmentProcessId).ToList();
            List<SurveyQuestion> surveyQuestionList = _surveyQuestionService.GetSurveyQuestionsForRecruitmentProcess(recruitmentProcessId).Where(q => q.ParentQuestionId == null).ToList();
            surveyElement = element.Descendants("Survey").FirstOrDefault();
            foreach (var question in surveyQuestionList)
            {
                if (question.SurveyQuestionTypes.Name == "Otwarte")
                {
                    SurveyAnswer answer = surveyAnswerList.Where(a => a.SurveyQuestionId == question.Id).FirstOrDefault();
                    surveyElement.Add(new XElement("Question", new XAttribute("type", "open"), new XAttribute("question", question.Question), new XAttribute("answer", answer.Answer)));
                }
                else if (question.SurveyQuestionTypes.Name == "Zamknięte jednokrotnego wyboru")
                {
                    SurveyAnswer answer = surveyAnswerList.Where(a => a.SurveyQuestionId == question.Id).FirstOrDefault();
                    surveyElement.Add(new XElement("Question", new XAttribute("type", "closedOne"), new XAttribute("question", question.Question), new XAttribute("answer", answer.Answer)));
                }
                else
                {
                    surveyElement.Add(new XElement("Question", new XAttribute("type", "closedMany"), new XAttribute("question", question.Question)));
                    List<SurveyAnswer> answersForQuestionList = surveyAnswerList.Where(a => a.SurveyQuestionId == question.Id).ToList();
                    XElement questionElement = surveyElement.Descendants("Question").Where(el => el.Attribute("question").Value == question.Question).FirstOrDefault();
                    foreach (var a in answersForQuestionList)
                    {
                        questionElement.Add(new XElement("Answer", a.Answer));
                    }
                }
            }
        }
        #endregion
    }
}