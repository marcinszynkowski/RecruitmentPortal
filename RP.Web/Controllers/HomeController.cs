using RP.AccountModule.Interfaces;
using RP.AdminModule.Interfaces;
using RP.Entities.Account;
using RP.Entities.Recruitment;
using RP.Entities.RecruitmentModule.ViewModels;
using RP.RecruitmentModule.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace RP.Controllers
{
    public class HomeController : Controller
    {
        #region Fields
        private readonly IUserService _userService;
        private readonly IRecruitmentProcessService _recruitmentProcessService;
        private readonly IFormOfEmploymentService _formOfEmploymentService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IProcessStatusService _processStatusService;
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly IRoleService _roleService;
        private readonly ISurveyAnswerService _surveyAnswerService;
        #endregion

        #region Constructor
        public HomeController(IUserService userService, IRecruitmentProcessService recruitmentProcessService, IFormOfEmploymentService formOfEmploymentService,
            IPaymentTypeService paymentTypeService, IProcessStatusService processStatusService, ISurveyQuestionService surveyQuestionService, ISurveyAnswerService surveyAnswerService,
            IRoleService roleService)
        {
            _userService = userService;
            _recruitmentProcessService = recruitmentProcessService;
            _formOfEmploymentService = formOfEmploymentService;
            _paymentTypeService = paymentTypeService;
            _processStatusService = processStatusService;
            _surveyQuestionService = surveyQuestionService;
            _surveyAnswerService = surveyAnswerService;
            _roleService = roleService;
        }
        #endregion

        public ActionResult Index()
        {
            FilterViewModel filterViewModel = new FilterViewModel()
            {
                CitiesViewModelList = new List<FilterByCityViewModel>(),
                CompaniesViewModelList = new List<FilterByCompanyViewModel>(),
                FormOfEmploymentViewModelList = new List<FilterByFormOfEmploymentViewModel>(),
                PositionsViewModelList = new List<FilterByPositionViewModel>(),
                PaymentTypesViewModelList = new List<FilterByPaymentTypeViewModel>()
            };
            foreach (var city in _recruitmentProcessService.GetAllRecruitmentProcesses().Select(x => x.Cities.Name).Distinct().ToList())
            {
                FilterByCityViewModel filter = new FilterByCityViewModel()
                {
                    City = city
                };
                filterViewModel.CitiesViewModelList.Add(filter);
            }
            foreach (var company in _recruitmentProcessService.GetAllRecruitmentProcesses().Select(x => x.Companies.Name).Distinct().ToList())
            {
                FilterByCompanyViewModel filter = new FilterByCompanyViewModel()
                {
                    Company = company
                };
                filterViewModel.CompaniesViewModelList.Add(filter);
            }
            foreach (var form in _recruitmentProcessService.GetAllRecruitmentProcesses().Select(x => x.FormOfEmployments.Name).Distinct().ToList())
            {
                FilterByFormOfEmploymentViewModel filter = new FilterByFormOfEmploymentViewModel()
                {
                    FormOfEmployment = form
                };
                filterViewModel.FormOfEmploymentViewModelList.Add(filter);
            }
            foreach (var position in _recruitmentProcessService.GetAllRecruitmentProcesses().Select(x => x.Positions.Name).Distinct().ToList())
            {
                FilterByPositionViewModel filter = new FilterByPositionViewModel()
                {
                    Position = position
                };
                filterViewModel.PositionsViewModelList.Add(filter);
            }
            foreach (var paymentType in _recruitmentProcessService.GetAllRecruitmentProcesses().Select(x => x.PaymentTypes.Name).Distinct().ToList())
            {
                FilterByPaymentTypeViewModel filter = new FilterByPaymentTypeViewModel()
                {
                    PaymentType = paymentType
                };
                filterViewModel.PaymentTypesViewModelList.Add(filter);
            }

            filterViewModel.RecruitmentProcessViewModelList = _recruitmentProcessService.GetFilteredRecruitmentProcesses(filterViewModel);
            return View(filterViewModel);
        }

        [HttpPost]
        public ActionResult Index(FilterViewModel filterViewModel)
        {
            filterViewModel.RecruitmentProcessViewModelList = _recruitmentProcessService.GetFilteredRecruitmentProcesses(filterViewModel);

            return View(filterViewModel);
        }

        public ActionResult Apply(int id)
        {
            ApplyViewModel applyViewModel = GetApplyViewModel(id);

            return View(applyViewModel);
        }

        [HttpPost]
        public ActionResult Apply(ApplyViewModel applyViewModel)
        {
            if (ModelState.IsValid && applyViewModel.SurveyClosedQuestionMultipleAnswersModelList.All(a => a.Answers.Any(i => i.IsCheked == true)))
            {
                if(applyViewModel.ApplyFilesViewModel.CV != null)
                {
                    try
                    {
                        foreach (var question in applyViewModel.SurveyOpenQuestionViewModelList)
                        {
                            _surveyAnswerService.CreateSurveyAnswer(applyViewModel.UserId, question.SurveyQuestionId, question.Answer);
                        }
                        foreach (var question in applyViewModel.SurveyClosedQuestionMultipleAnswersModelList)
                        {
                            foreach (var answer in question.Answers)
                            {
                                if (answer.IsCheked)
                                {
                                    _surveyAnswerService.CreateSurveyAnswer(applyViewModel.UserId, question.SurveyQuestionId, answer.Answer);
                                }
                            }
                        }
                        foreach (var question in applyViewModel.SurveyClosedQuestionOneAnswerViewModelList)
                        {
                            foreach (var answer in question.Answers)
                            {
                                if (answer.AnswerId == question.SelectedAnswer)
                                {
                                    _surveyAnswerService.CreateSurveyAnswer(applyViewModel.UserId, question.SurveyQuestionId, answer.Answer);
                                }
                            }
                        }

                        ApplyToRecruitmentProcess(applyViewModel);

                        return View("ApplyConfirmed");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błedy, proszę powtórzyć aplikację");
                    }

                    return RedirectToAction("Apply", applyViewModel.RecruitmentProcessId);
                }
                else
                {
                    ModelState.AddModelError("", "Proszę zamieścić CV");
                }
            }
            else
            {
                ModelState.AddModelError("", "Proszę wypełnić ankietę");
            }
            applyViewModel = GetApplyViewModel(applyViewModel.RecruitmentProcessId);
            return View(applyViewModel);
        }

        private ApplyViewModel GetApplyViewModel(int id)
        {
            List<SurveyQuestion> surveyQuestionList = _surveyQuestionService.GetSurveyQuestionsForRecruitmentProcess(id).Where(q => q.ParentQuestionId == null).ToList();
            User user = _userService.GetUserByEmail(User.Identity.Name);
            RecruitmentProcess recruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessById(id);
            IList<SurveyOpenQuestionViewModel> surveyOpenQuestionViewModelList = new List<SurveyOpenQuestionViewModel>();
            IList<SurveyClosedQuestionOneAnswerViewModel> surveyClosedQuestionOneAnswerViewModelList = new List<SurveyClosedQuestionOneAnswerViewModel>();
            IList<SurveyClosedQuestionMultipleAnswersViewModel> surveyClosedQuestionMultipleAnswersViewModelList = new List<SurveyClosedQuestionMultipleAnswersViewModel>();

            ApplyViewModel applyViewModel = new ApplyViewModel
            {
                RecruitmentProcessId = id,
                UserId = user.Id,
                SurveyClosedQuestionMultipleAnswersModelList = new List<SurveyClosedQuestionMultipleAnswersViewModel>(),
                SurveyClosedQuestionOneAnswerViewModelList = new List<SurveyClosedQuestionOneAnswerViewModel>(),
                SurveyOpenQuestionViewModelList = new List<SurveyOpenQuestionViewModel>()
            };
            foreach (var question in surveyQuestionList)
            {
                if (question.SurveyQuestionTypes.Name == "Otwarte")
                {
                    SurveyOpenQuestionViewModel surveyOpenQuestionViewModel = new SurveyOpenQuestionViewModel()
                    {
                        SurveyQuestionId = question.Id,
                        Question = question.Question
                    };
                    applyViewModel.SurveyOpenQuestionViewModelList.Add(surveyOpenQuestionViewModel);
                }
                else if (question.SurveyQuestionTypes.Name == "Zamknięte jednokrotnego wyboru")
                {
                    List<SurveyQuestion> answerList = _surveyQuestionService.GetAllAnswers(question.Id);
                    SurveyClosedQuestionOneAnswerViewModel surveyClosedQuestionOneAnswerViewModel = new SurveyClosedQuestionOneAnswerViewModel()
                    {
                        Question = question.Question,
                        SurveyQuestionId = question.Id,
                        Answers = new List<AnswerViewModel>()
                    };
                    foreach (var answer in answerList)
                    {
                        AnswerViewModel answerViewModel = new AnswerViewModel()
                        {
                            Answer = answer.Question,
                            IsCheked = false,
                            AnswerId = answer.Id
                        };

                        surveyClosedQuestionOneAnswerViewModel.Answers.Add(answerViewModel);
                    }
                    applyViewModel.SurveyClosedQuestionOneAnswerViewModelList.Add(surveyClosedQuestionOneAnswerViewModel);
                }
                else if (question.SurveyQuestionTypes.Name == "Zamknięte wielokrotnego wyboru")
                {
                    List<SurveyQuestion> answerList = _surveyQuestionService.GetAllAnswers(question.Id);
                    SurveyClosedQuestionMultipleAnswersViewModel SurveyClosedQuestionMultipleAnswersViewModel = new SurveyClosedQuestionMultipleAnswersViewModel()
                    {
                        Question = question.Question,
                        SurveyQuestionId = question.Id,
                        Answers = new List<AnswerViewModel>()
                    };
                    foreach (var answer in answerList)
                    {
                        AnswerViewModel answerViewModel = new AnswerViewModel()
                        {
                            Answer = answer.Question,
                            IsCheked = false,
                            AnswerId = answer.Id
                        };

                        SurveyClosedQuestionMultipleAnswersViewModel.Answers.Add(answerViewModel);
                    }
                    applyViewModel.SurveyClosedQuestionMultipleAnswersModelList.Add(SurveyClosedQuestionMultipleAnswersViewModel);
                }
            }
            return applyViewModel;
        }

        private void ApplyToRecruitmentProcess(ApplyViewModel applyViewModel)
        {
            {
                var recruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessById(applyViewModel.RecruitmentProcessId);
                _userService.AddRecruitmentProcessToUser(User.Identity.Name, recruitmentProcess);
                SendApplication(applyViewModel);
            }
        }

        private void SendApplication(ApplyViewModel applyViewModel)
        {
            User user = _userService.GetUserById(applyViewModel.UserId);
            RecruitmentProcess recruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessById(applyViewModel.RecruitmentProcessId);

            SendApplicationEmail(recruitmentProcess, user, applyViewModel);
        }

        private void SendApplicationEmail(RecruitmentProcess recruitmentProcess, User user, ApplyViewModel applyViewModel)
        {
            SmtpClient smtpClient = new SmtpClient();

            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(recruitmentProcess.Email));
            mailMessage.Subject = string.Format("Złożono aplikację, kod procesu: {0}", recruitmentProcess.ProcessCode);
            mailMessage.Body = string.Format("Witaj {0}<BR/>Użytkownik: {1} właśnie zaaplikował na Twoją ofertę o kodzie {2} poprzez serwis Recruitment Portal", recruitmentProcess.Email, user.Email, recruitmentProcess.ProcessCode);
            mailMessage.IsBodyHtml = true;
            if (applyViewModel.ApplyFilesViewModel.MotivationLetter != null)
            {
                string fileName = Path.GetFileName(applyViewModel.ApplyFilesViewModel.MotivationLetter.FileName);
                mailMessage.Attachments.Add(new Attachment(applyViewModel.ApplyFilesViewModel.MotivationLetter.InputStream, fileName));
            }

            if (applyViewModel.ApplyFilesViewModel.CV != null)
            {
                string fileName = Path.GetFileName(applyViewModel.ApplyFilesViewModel.CV.FileName);
                mailMessage.Attachments.Add(new Attachment(applyViewModel.ApplyFilesViewModel.CV.InputStream, fileName));
            }
            smtpClient.Send(mailMessage);
        }

        public ActionResult VirtualCV(Guid guid)
        {
            User user = _userService.GetUserByVirtualCVAdress(guid);
            return View(user);
        }
    }
}