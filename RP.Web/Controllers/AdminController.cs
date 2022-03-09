using RP.AccountModule.Interfaces;
using RP.AdminModule.Interfaces;
using RP.Entities.Account;
using RP.Entities.AdminModule.ViewModels;
using RP.Entities.Recruitment;
using RP.Entities.User;
using RP.RecruitmentModule.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Mvc;

namespace RP.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        #region Fields
        private readonly IXmlService _xmlService;
        private readonly IRecruitmentProcessService _recruitmentProcessService;
        private readonly IPaymentTypeService _paymentTypeService;
        private readonly IFormOfEmploymentService _formOfEmploymentService;
        private readonly ICompanyService _companyService;
        private readonly ICityService _cityService;
        private readonly IPositionService _positionService;
        private readonly IProcessStatusService _processStatusService;
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ISurveyQuestionTypeService _surveyQuestionTypeService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ICourseTypeService _courseTypeService;
        private readonly ICourseKindService _courseKindService;
        private readonly IForeignLanguageService _foreignLanguageService;
        private readonly IForeignLanguageLevelService _foreignLanguageLevelService;
        private readonly IDrivingLicenseService _drivingLicenseService;
        #endregion

        #region Constructor
        public AdminController(IXmlService xmlService, IRecruitmentProcessService recruitmentProcessService, IPaymentTypeService paymentTypeService,
            IFormOfEmploymentService formOfEmploymentService, ICompanyService companyService, ICityService cityService, IPositionService positionService, IProcessStatusService processStatusService,
            ISurveyQuestionService surveyQuestionService, ISurveyQuestionTypeService surveyQuestionTypeService, IUserService userService, IRoleService roleService,
            ICourseTypeService courseTypeService, ICourseKindService courseKindService, IForeignLanguageService foreignLanguageService, IForeignLanguageLevelService foreignLanguageLevelService,
            IDrivingLicenseService drivingLicenseService)
        {
            _xmlService = xmlService;
            _recruitmentProcessService = recruitmentProcessService;
            _paymentTypeService = paymentTypeService;
            _formOfEmploymentService = formOfEmploymentService;
            _companyService = companyService;
            _cityService = cityService;
            _positionService = positionService;
            _processStatusService = processStatusService;
            _surveyQuestionService = surveyQuestionService;
            _surveyQuestionTypeService = surveyQuestionTypeService;
            _userService = userService;
            _roleService = roleService;
            _courseTypeService = courseTypeService;
            _courseKindService = courseKindService;
            _foreignLanguageService = foreignLanguageService;
            _foreignLanguageLevelService = foreignLanguageLevelService;
            _drivingLicenseService = drivingLicenseService;

        }
        #endregion

        #region Import/Export
        public ActionResult ImportRecruitmentProcessesFromErpLN()
        {
            _xmlService.ImportRecruitmentProcesses();
            return View();
        }

        public ActionResult ExportApplicantsDataToErpLn()
        {
            _xmlService.ExportApplicantsData();
            return View();
        }
        #endregion

        #region Recruitment processes
        public ActionResult MenageRecruitmentProcesses()
        {
            List<RecruitmentProcess> list = _recruitmentProcessService.GetAllRecruitmentProcesses();
            return View(list);
        }

        public ActionResult AplicantsList(int id)
        {
            AplicantsListViewModel aplicantsListViewModel = new AplicantsListViewModel
            {
                RecruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessById(id)
            };
            return View(aplicantsListViewModel);
        }

        public ActionResult AddRecruitmentProcess()
        {
            ViewBag.PaymentTypes = GetPaymentTypes();
            ViewBag.FormOfEmployment = GetFormOfEmployment();
            ViewBag.Company = GetCompanies();
            ViewBag.City = GetCities();
            ViewBag.Position = GetPositions();
            return View();
        }

        private List<SelectListItem> GetPaymentTypes()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<PaymentType> paymentTypeList = _paymentTypeService.GetAllPaymentTypes().OrderBy(x => x.Name).ToList();

            foreach (var temp in paymentTypeList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        private List<SelectListItem> GetFormOfEmployment()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<FormOfEmployment> formOfEmploymentList = _formOfEmploymentService.GetAllFormOfEmployment().OrderBy(x => x.Name).ToList();

            foreach (var temp in formOfEmploymentList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        private List<SelectListItem> GetCompanies()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<Company> companiesList = _companyService.GetAllCompanies().OrderBy(x => x.Name).ToList();

            foreach (var temp in companiesList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        private List<SelectListItem> GetCities()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<City> citiesList = _cityService.GetAllCities().OrderBy(x => x.Name).ToList();

            foreach (var temp in citiesList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        private List<SelectListItem> GetPositions()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<Position> positionsList = _positionService.GetAllPositions().OrderBy(x => x.Name).ToList();

            foreach (var temp in positionsList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        [HttpPost]
        public ActionResult AddRecruitmentProcess(AddRecruitmentProcessViewModel addRecruitmentProcessViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_recruitmentProcessService.GetRecruitmentProcessByProcessCode(addRecruitmentProcessViewModel.ProcessCode) == null)
                    {
                        try
                        {
                            _recruitmentProcessService.CreateRecruitmentProcess(addRecruitmentProcessViewModel.ProcessCode, addRecruitmentProcessViewModel.CompanyId, addRecruitmentProcessViewModel.CityId,
                                addRecruitmentProcessViewModel.PositionId, addRecruitmentProcessViewModel.Tasks, addRecruitmentProcessViewModel.Requirements, addRecruitmentProcessViewModel.Offer, addRecruitmentProcessViewModel.FormOfEmploymentId,
                                addRecruitmentProcessViewModel.PaymentTypeId, addRecruitmentProcessViewModel.Payment, addRecruitmentProcessViewModel.Email, addRecruitmentProcessViewModel.Phone);
                            RecruitmentProcess recruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessByProcessCode(addRecruitmentProcessViewModel.ProcessCode);
                            return RedirectToAction("MenageSurveyQuestions", "Admin", new { recruitmentProcessId = recruitmentProcess.Id });
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie procesu.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Proces rekrutacyjny o tym kodzie już istnieje.");
                    }
                }
            }
            ViewBag.PaymentTypes = GetPaymentTypes();
            ViewBag.FormOfEmployment = GetFormOfEmployment();
            ViewBag.Company = GetCompanies();
            ViewBag.City = GetCities();
            ViewBag.Position = GetPositions();
            return View();
        }

        public ActionResult EditRecruitmentProcess(int id)
        {
            {
                RecruitmentProcess recruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessById(id);
                EditRecruitmentProcessViewModel editRecruitmentProcessViewModel = new EditRecruitmentProcessViewModel
                {
                    Id = recruitmentProcess.Id,
                    ProcessCode = recruitmentProcess.ProcessCode,
                    CompanyId = recruitmentProcess.CompanyId,
                    CityId = recruitmentProcess.CityId,
                    PositionId = recruitmentProcess.PositionId,
                    Tasks = recruitmentProcess.Tasks,
                    Requirements = recruitmentProcess.Requirements,
                    Offer = recruitmentProcess.Offer,
                    FormOfEmploymentId = recruitmentProcess.FormOfEmploymentId,
                    PaymentTypeId = recruitmentProcess.PaymentTypeId,
                    Payment = recruitmentProcess.Payment,
                    Email = recruitmentProcess.Email,
                    Phone = recruitmentProcess.Phone,
                    ProcessStatusId = recruitmentProcess.ProcessStatusId
                };
                ViewBag.PaymentTypes = GetPaymentTypes();
                ViewBag.FormOfEmployment = GetFormOfEmployment();
                ViewBag.ProcessStatus = GetProcessStatus();
                ViewBag.Company = GetCompanies();
                ViewBag.City = GetCities();
                ViewBag.Position = GetPositions();
                return View(editRecruitmentProcessViewModel);
            }
        }

        private List<SelectListItem> GetProcessStatus()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<ProcessStatus> processStatusList = _processStatusService.GetAllProcessStatus();

            foreach (var temp in processStatusList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        [HttpPost]
        public ActionResult EditRecruitmentProcess(EditRecruitmentProcessViewModel editRecruitmentProcessViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    RecruitmentProcess recruitmentProcess = _recruitmentProcessService.GetRecruitmentProcessById(editRecruitmentProcessViewModel.Id);
                    if (_recruitmentProcessService.GetRecruitmentProcessByProcessCode(editRecruitmentProcessViewModel.ProcessCode) != null &&
                            _recruitmentProcessService.GetRecruitmentProcessByProcessCode(editRecruitmentProcessViewModel.ProcessCode).Id != editRecruitmentProcessViewModel.Id)
                    {
                        ModelState.AddModelError("", "Proces rekrutacyjny o tym kodzie już istnieje.");
                        return View();
                    }
                    RecruitmentProcess editedRecruitmentProcess = new RecruitmentProcess()
                    {
                        Id = recruitmentProcess.Id,
                        CityId = editRecruitmentProcessViewModel.CityId,
                        CompanyId = editRecruitmentProcessViewModel.CompanyId,
                        Email = editRecruitmentProcessViewModel.Email,
                        FormOfEmploymentId = editRecruitmentProcessViewModel.FormOfEmploymentId,
                        Offer = editRecruitmentProcessViewModel.Offer,
                        Payment = editRecruitmentProcessViewModel.Payment,
                        PaymentTypeId = editRecruitmentProcessViewModel.PaymentTypeId,
                        Phone = editRecruitmentProcessViewModel.Phone,
                        PositionId = editRecruitmentProcessViewModel.PositionId,
                        ProcessCode = editRecruitmentProcessViewModel.ProcessCode,
                        Requirements = editRecruitmentProcessViewModel.Requirements,
                        ProcessStatusId = editRecruitmentProcessViewModel.ProcessStatusId,
                        Tasks = editRecruitmentProcessViewModel.Tasks
                    };
                    try
                    {
                        _recruitmentProcessService.EditRecruitmentProcess(editedRecruitmentProcess.Id, editedRecruitmentProcess);
                        return RedirectToAction("MenageRecruitmentProcesses", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie procesu.");
                    }

                }
            }
            return View();
        }

        public ActionResult DeleteRecruitmentProcess(int id)
        {
            {
                DeleteRecruitmentProcessViewModel deleteRecruitmentProcessViewModel = new DeleteRecruitmentProcessViewModel
                {
                    ProcessCode = _recruitmentProcessService.GetRecruitmentProcessById(id).ProcessCode
                };
                return View(deleteRecruitmentProcessViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteRecruitmentProcess(DeleteRecruitmentProcessViewModel deleteRecruitmentProcessViewModel)
        {
            {
                _recruitmentProcessService.DeleteRecruitmentProcess(deleteRecruitmentProcessViewModel.ProcessCode);
                return RedirectToAction("MenageRecruitmentProcesses", "Admin");
            }
        }
        #endregion

        #region Survey questions
        public ActionResult MenageSurveyQuestions(int recruitmentProcessId)
        {
            List<SurveyQuestion> list = new List<SurveyQuestion>();

            list = _surveyQuestionService.GetSurveyQuestionsForRecruitmentProcess(recruitmentProcessId);

            MenageSurveyQuestionsViewModel menageSurveyQuestionsViewModel = new MenageSurveyQuestionsViewModel()
            {
                SurveyQuestionList = list,
                RecruitmentProcessId = recruitmentProcessId,
                RecruitmentProcessCode = _recruitmentProcessService.GetRecruitmentProcessById(recruitmentProcessId).ProcessCode
            };
            return View(menageSurveyQuestionsViewModel);
        }

        public ActionResult AddSurveyQuestion(int recruitmentProcessId)
        {
            AddSurveyQuestionViewModel addSurveyQuestionViewModel = new AddSurveyQuestionViewModel()
            {
                RecruitmentProcessId = recruitmentProcessId,
                RecruitmentProcessCode = _recruitmentProcessService.GetRecruitmentProcessById(recruitmentProcessId).ProcessCode
            };
            ViewBag.QuestionType = GetQuestionTypes();
            return View(addSurveyQuestionViewModel);
        }

        private List<SelectListItem> GetQuestionTypes()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<SurveyQuestionType> surveyQuestionTypeList = _surveyQuestionTypeService.GetAllSurveyQuestionTypes().OrderBy(x => x.Name).ToList();

            foreach (var temp in surveyQuestionTypeList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        [HttpPost]
        public ActionResult AddSurveyQuestion(AddSurveyQuestionViewModel addSurveyQuestionViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    try
                    {
                        _surveyQuestionService.CreateSurveyQuestion(addSurveyQuestionViewModel.RecruitmentProcessId, addSurveyQuestionViewModel.Question, addSurveyQuestionViewModel.QuestionTypeId);

                        return RedirectToAction("MenageSurveyQuestions", "Admin", new { recruitmentProcessId = addSurveyQuestionViewModel.RecruitmentProcessId });
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie pytania.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditSurveyQuestion(int id, int processCode)
        {
            {
                SurveyQuestion surveyQuestion = _surveyQuestionService.GetSurveyQuestionById(id);
                EditSurveyQuestionViewModel editSurveyQuestionViewModel = new EditSurveyQuestionViewModel
                {
                    Question = surveyQuestion.Question,
                    RecruitmentProcessId = surveyQuestion.RecruitmentProcessId,
                    RecruitmentProcessCode = processCode,
                    SurveyQuestionId = id
                };
                return View(editSurveyQuestionViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditSurveyQuestion(EditSurveyQuestionViewModel editSurveyQuestionViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    try
                    {
                        _surveyQuestionService.SetSurveyQuestion(editSurveyQuestionViewModel.SurveyQuestionId, editSurveyQuestionViewModel.Question);
                        return RedirectToAction("MenageSurveyQuestions", "Admin", new { recruitmentProcessId = editSurveyQuestionViewModel.RecruitmentProcessId });
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie pytania.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteSurveyQuestion(int id, int processCode)
        {
            {
                SurveyQuestion surveyQuestion = _surveyQuestionService.GetSurveyQuestionById(id);
                DeleteSurveyQuestionViewModel deleteSurveyQuestionViewModel = new DeleteSurveyQuestionViewModel
                {
                    Question = surveyQuestion.Question,
                    RecruitmentProcessCode = processCode,
                    RecruitmentProcessId = surveyQuestion.RecruitmentProcessId,
                    SurveyQuestionId = id
                };
                return View(deleteSurveyQuestionViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteSurveyQuestion(DeleteSurveyQuestionViewModel deleteSurveyQuestionViewModel)
        {
            {
                _surveyQuestionService.DeleteSurveyQuestion(deleteSurveyQuestionViewModel.SurveyQuestionId);
                return RedirectToAction("MenageSurveyQuestions", "Admin", new { recruitmentProcessId = deleteSurveyQuestionViewModel.RecruitmentProcessId });
            }
        }

        public ActionResult MenageAnswers(int id, int recruitmentProcessId, int questionTypeId)
        {
            List<SurveyQuestion> list = new List<SurveyQuestion>();
            MenageAnswersViewModel menageAnswersViewModel = new MenageAnswersViewModel();

            try
            {
                list = _surveyQuestionService.GetSurveyQuestionsForRecruitmentProcess(recruitmentProcessId).Where(q => q.ParentQuestionId == id).ToList();
                menageAnswersViewModel.SurveyQuestionList = list;
                menageAnswersViewModel.RecruitmentProcessId = recruitmentProcessId;
                menageAnswersViewModel.SurveyQuestionId = id;
                menageAnswersViewModel.SurveyQuestionTypeId = questionTypeId;
            }
            catch
            {
            }

            return View(menageAnswersViewModel);
        }

        public ActionResult AddAnswer(int id, int recruitmentProcessId, int questionTypeId)
        {
            AddAnswerViewModel addAnswerViewModel = new AddAnswerViewModel()
            {
                QuestionId = id,
                RecruitmentProcessId = recruitmentProcessId,
                SurveyQuestionTypeId = questionTypeId,
                SurveyQuestionName = _surveyQuestionService.GetSurveyQuestionById(id).Question
            };

            return View(addAnswerViewModel);
        }

        [HttpPost]
        public ActionResult AddAnswer(AddAnswerViewModel addAnswerViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    try
                    {
                        _surveyQuestionService.CreateAnswer(addAnswerViewModel.RecruitmentProcessId, addAnswerViewModel.QuestionId, addAnswerViewModel.SurveyQuestionTypeId, addAnswerViewModel.Answer);

                        return RedirectToAction("MenageAnswers", "Admin", new { id = addAnswerViewModel.QuestionId, recruitmentProcessId = addAnswerViewModel.RecruitmentProcessId, questionTypeId = addAnswerViewModel.SurveyQuestionTypeId });
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie odpowiedzi.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditAnswer(int id, int recruitmentProcessId, int questionId, int questionTypeId)
        {
            {
                SurveyQuestion surveyQuestion = _surveyQuestionService.GetSurveyQuestionById(id);
                EditAnswerViewModel editAnswerViewModel = new EditAnswerViewModel
                {
                    Answer = surveyQuestion.Question,
                    RecruitmentProcessId = surveyQuestion.RecruitmentProcessId,
                    AnswerId = id,
                    SurveyQuestionName = _surveyQuestionService.GetSurveyQuestionById(questionId).Question,
                    SurveyQuestionId = questionId,
                    SurveyQuestionTypeId = questionTypeId
                };
                return View(editAnswerViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditAnswer(EditAnswerViewModel editAnswerViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    try
                    {
                        _surveyQuestionService.SetSurveyQuestion(editAnswerViewModel.AnswerId, editAnswerViewModel.Answer);
                        return RedirectToAction("MenageAnswers", "Admin", new { id = editAnswerViewModel.SurveyQuestionId, recruitmentProcessId = editAnswerViewModel.RecruitmentProcessId, questionTypeId = editAnswerViewModel.SurveyQuestionTypeId });
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie odpowiedzi.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteAnswer(int id, int questionId, int questionTypeId)
        {
            {
                SurveyQuestion surveyQuestion = _surveyQuestionService.GetSurveyQuestionById(id);
                DeleteAnswerViewModel deleteAnswerViewModel = new DeleteAnswerViewModel
                {
                    Answer = surveyQuestion.Question,
                    AnswerId = id,
                    RecruitmentProcessId = surveyQuestion.RecruitmentProcessId,
                    SurveyQuestionId = questionId,
                    SurveyQuestionTypeId = questionTypeId,
                    SurveyQuestionName = _surveyQuestionService.GetSurveyQuestionById(questionId).Question,
                };
                return View(deleteAnswerViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteAnswer(DeleteAnswerViewModel deleteAnswerViewModel)
        {
            {
                _surveyQuestionService.DeleteAnswer(deleteAnswerViewModel.AnswerId);
                return RedirectToAction("MenageAnswers", "Admin", new { id = deleteAnswerViewModel.SurveyQuestionId, recruitmentProcessId = deleteAnswerViewModel.RecruitmentProcessId, questionTypeId = deleteAnswerViewModel.SurveyQuestionTypeId });
            }
        }
        #endregion

        #region Form of employment
        public ActionResult MenageFormOfEmployment()
        {
            List<FormOfEmployment> list = _formOfEmploymentService.GetAllFormOfEmployment();
            return View(list);
        }

        public ActionResult AddFormOfEmployment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddFormOfEmployment(AddFormOfEmploymentViewModel addFormOfEmploymentViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_formOfEmploymentService.GetFormOfEmploymentByName(addFormOfEmploymentViewModel.Name) == null)
                    {
                        try
                        {
                            _formOfEmploymentService.CreateFormOfEmployment(addFormOfEmploymentViewModel.Name);
                            return RedirectToAction("MenageFormOfEmployment", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie formy.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Forma zatrudnienia o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditFormOfEmployment(int id)
        {
            {
                FormOfEmployment formOfEmployment = _formOfEmploymentService.GetFormOfEmploymentById(id);
                EditFormOfEmploymentViewModel editFormOfEmploymentViewModel = new EditFormOfEmploymentViewModel
                {
                    Name = _formOfEmploymentService.GetFormOfEmploymentById(id).Name
                };
                return View(editFormOfEmploymentViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditFormOfEmployment(EditFormOfEmploymentViewModel editFormOfEmploymentViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    FormOfEmployment formOfEmployment = _formOfEmploymentService.GetFormOfEmploymentByName(editFormOfEmploymentViewModel.Name);

                    if (formOfEmployment != null && formOfEmployment.Id != editFormOfEmploymentViewModel.Id)
                    {
                        ModelState.AddModelError("", "Forma zatrudnenia o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _formOfEmploymentService.SetFormOfEmploymentName(editFormOfEmploymentViewModel.Id, editFormOfEmploymentViewModel.Name);
                        return RedirectToAction("MenageFormOfEmployment", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie formy.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteFormOfEmployment(int id)
        {
            {
                DeleteFormOfEmploymentViewModel deleteFormOfEmploymentViewModel = new DeleteFormOfEmploymentViewModel
                {
                    Name = _formOfEmploymentService.GetFormOfEmploymentById(id).Name,
                    Id = id
                };
                return View(deleteFormOfEmploymentViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteFormOfEmployment(DeleteFormOfEmploymentViewModel deleteFormOfEmploymentViewModel)
        {
            {
                _formOfEmploymentService.DeleteFormOfEmployment(deleteFormOfEmploymentViewModel.Id);
                return RedirectToAction("MenageFormOfEmployment", "Admin");
            }
        }
        #endregion

        #region Cities
        public ActionResult MenageCities()
        {
            List<City> list = _cityService.GetAllCities();
            return View(list);
        }

        public ActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCity(AddCityViewModel addCityViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_cityService.GetCityByName(addCityViewModel.Name) == null)
                    {
                        try
                        {
                            _cityService.CreateCity(addCityViewModel.Name);
                            return RedirectToAction("MenageCities", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie miasta.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Miasto o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditCity(int id)
        {
            {
                City city = _cityService.GetCityById(id);
                EditCityViewModel editCityViewModel = new EditCityViewModel
                {
                    Name = _cityService.GetCityById(id).Name
                };
                return View(editCityViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditCity(EditCityViewModel editCityViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    City city = _cityService.GetCityByName(editCityViewModel.Name);

                    if (city != null && city.Id != editCityViewModel.Id)
                    {
                        ModelState.AddModelError("", "Miasto o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _cityService.SetCityName(editCityViewModel.Id, editCityViewModel.Name);
                        return RedirectToAction("MenageCities", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie miasta.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteCity(int id)
        {
            {
                DeleteCityViewModel deleteCityViewModel = new DeleteCityViewModel
                {
                    Name = _cityService.GetCityById(id).Name,
                    Id = id
                };
                return View(deleteCityViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteCity(DeleteCityViewModel deleteCityViewModel)
        {
            {
                _cityService.DeleteCity(deleteCityViewModel.Name);
                return RedirectToAction("MenageCities", "Admin");
            }
        }
        #endregion

        #region Companies
        public ActionResult MenageCompanies()
        {
            List<Company> list = _companyService.GetAllCompanies();
            return View(list);
        }

        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCompany(AddCompanyViewModel addCompanyViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_companyService.GetCompanyByName(addCompanyViewModel.Name) == null)
                    {
                        try
                        {
                            _companyService.CreateCompany(addCompanyViewModel.Name);
                            return RedirectToAction("MenageCompanies", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie firmy.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Firma o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditCompany(int id)
        {
            {
                Company company = _companyService.GetCompanyById(id);
                EditCompanyViewModel editCompanyViewModel = new EditCompanyViewModel
                {
                    Name = _companyService.GetCompanyById(id).Name
                };
                return View(editCompanyViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditCompany(EditCompanyViewModel editCompanyViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    Company company = _companyService.GetCompanyByName(editCompanyViewModel.Name);

                    if (company != null && company.Id != editCompanyViewModel.Id)
                    {
                        ModelState.AddModelError("", "Firma o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _companyService.SetCompanyName(editCompanyViewModel.Id, editCompanyViewModel.Name);
                        return RedirectToAction("MenageCompanies", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie firmy.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteCompany(int id)
        {
            {
                DeleteCompanyViewModel deleteCompanyViewModel = new DeleteCompanyViewModel
                {
                    Name = _companyService.GetCompanyById(id).Name,
                    Id = id
                };
                return View(deleteCompanyViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteCompany(DeleteCompanyViewModel deleteCompanyViewModel)
        {
            {
                _companyService.DeleteCompany(deleteCompanyViewModel.Name);
                return RedirectToAction("MenageCompanies", "Admin");
            }
        }
        #endregion

        #region Positions
        public ActionResult MenagePositions()
        {
            List<Position> list = _positionService.GetAllPositions();
            return View(list);
        }

        public ActionResult AddPosition()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPosition(AddPositionViewModel addPositionViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_positionService.GetPositionByName(addPositionViewModel.Name) == null)
                    {
                        try
                        {
                            _positionService.CreatePosition(addPositionViewModel.Name);
                            return RedirectToAction("MenagePositions", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie stanowiska.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Stanowisko o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditPosition(int id)
        {
            {
                Position position = _positionService.GetPositionById(id);
                EditPositionViewModel editPositionViewModel = new EditPositionViewModel
                {
                    Name = _positionService.GetPositionById(id).Name
                };
                return View(editPositionViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditPosition(EditPositionViewModel editPositionViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    Position position = _positionService.GetPositionByName(editPositionViewModel.Name);

                    if (position != null && position.Id != editPositionViewModel.Id)
                    {
                        ModelState.AddModelError("", "Stanowisko o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _positionService.SetPositionName(editPositionViewModel.Id, editPositionViewModel.Name);
                        return RedirectToAction("MenagePositions", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie stanowiska.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeletePosition(int id)
        {
            {
                DeletePositionViewModel deletePositionViewModel = new DeletePositionViewModel
                {
                    Name = _positionService.GetPositionById(id).Name,
                    Id = id
                };
                return View(deletePositionViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeletePosition(DeletePositionViewModel deletePositionViewModel)
        {
            {
                _positionService.DeletePosition(deletePositionViewModel.Name);
                return RedirectToAction("MenagePositions", "Admin");
            }
        }
        #endregion

        #region Payment types
        public ActionResult MenagePaymentTypes()
        {
            List<PaymentType> list = _paymentTypeService.GetAllPaymentTypes();
            return View(list);
        }

        public ActionResult AddPaymentType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPaymentType(AddPaymentTypeViewModel addPaymentTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_paymentTypeService.GetPaymentTypeByName(addPaymentTypeViewModel.Name) == null)
                    {
                        try
                        {
                            _paymentTypeService.CreatePaymentType(addPaymentTypeViewModel.Name);
                            return RedirectToAction("MenagePaymentTypes", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie rodzaju.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Rodzaj wynagrodzenia o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditPaymentType(int id)
        {
            {
                PaymentType paymentType = _paymentTypeService.GetPaymentTypeById(id);
                EditPaymentTypeViewModel editPaymentTypeViewModel = new EditPaymentTypeViewModel
                {
                    Name = _paymentTypeService.GetPaymentTypeById(id).Name
                };
                return View(editPaymentTypeViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditPaymentType(EditPaymentTypeViewModel editPaymentTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    PaymentType paymentType = _paymentTypeService.GetPaymentTypeByName(editPaymentTypeViewModel.Name);

                    if (paymentType != null && paymentType.Id != editPaymentTypeViewModel.Id)
                    {
                        ModelState.AddModelError("", "Rodzaj wynagrodzenia o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _paymentTypeService.SetPaymentTypeName(editPaymentTypeViewModel.Id, editPaymentTypeViewModel.Name);
                        return RedirectToAction("MenagePaymentTypes", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie rodzaju.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeletePaymentType(int id)
        {
            {
                DeletePaymentTypeViewModel deletePaymentTypeViewModel = new DeletePaymentTypeViewModel
                {
                    Name = _paymentTypeService.GetPaymentTypeById(id).Name,
                    Id = id
                };
                return View(deletePaymentTypeViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeletePaymentType(DeletePaymentTypeViewModel deletePaymentTypeViewModel)
        {
            {
                _paymentTypeService.DeletePaymentType(deletePaymentTypeViewModel.Id);
                return RedirectToAction("MenagePaymentTypes", "Admin");
            }
        }
        #endregion

        #region Users
        public ActionResult MenageUsers()
        {
            List<User> list = _userService.GetAllUsers();
            return View(list);
        }

        public ActionResult MoreInfoUser(int id)
        {
            MoreInfoUserViewModel moreInfoUserViewModel = new MoreInfoUserViewModel
            {
                User = _userService.GetUserById(id),
                PersonalData = _userService.GetUserPersonalDataById(id),
                EducationLevel = _userService.GetUserEducationLevelById(id)
            };
            return View(moreInfoUserViewModel);
        }

        public ActionResult SendEmailUser(string email)
        {
            SendEmailUserViewModel sendEmailUserViewModel = new SendEmailUserViewModel()
            {
                Email = email
            };
            return View(sendEmailUserViewModel);
        }

        [HttpPost]
        public ActionResult SendEmailUser(SendEmailUserViewModel sendEmailUserViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SmtpClient smtpClient = new SmtpClient();

                    MailMessage mailMessage = new MailMessage();
                    mailMessage.To.Add(new MailAddress(sendEmailUserViewModel.Email));
                    mailMessage.Subject = sendEmailUserViewModel.Subject;
                    mailMessage.Body = sendEmailUserViewModel.Body;
                    mailMessage.IsBodyHtml = true;

                    smtpClient.Send(mailMessage);
                    ModelState.AddModelError("", "Wiadomość została wysłana.");
                }
                catch
                {
                    ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić wysyłanie wadomości.");
                    return View(sendEmailUserViewModel);
                }
            }
            return View(sendEmailUserViewModel);
        }

        public ActionResult SendEmailUsers()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmailUsers(SendEmailUsersViewModel sendEmailUsersViewModel)
        {
            if (ModelState.IsValid)
            {
                string email = string.Empty;
                try
                {
                    SmtpClient smtpClient = new SmtpClient();

                    MailMessage mailMessage = new MailMessage();

                    List<User> usersList = _userService.GetAllUsers();

                    foreach (var user in usersList)
                    {
                        email = user.Email;

                        mailMessage.To.Add(new MailAddress(user.Email));
                        mailMessage.Subject = sendEmailUsersViewModel.Subject;
                        mailMessage.Body = sendEmailUsersViewModel.Body;
                        mailMessage.IsBodyHtml = true;

                        smtpClient.Send(mailMessage);
                    }

                    ModelState.AddModelError("", "Wiadomości zostały wysłane.");
                }
                catch
                {
                    ModelState.AddModelError("", string.Format("Wystąpiły błędy podczas wysyłania wiadomości do {0}, proszę ponowić wysyłanie wadomości.", email));
                    return View(sendEmailUsersViewModel);
                }
            }
            return View();
        }

        public ActionResult AddUser()
        {
            ViewBag.Roles = GetRoles();
            return View();
        }

        private List<SelectListItem> GetRoles()
        {
            List<SelectListItem> ls = new List<SelectListItem>();

            List<Role> rolesList = _roleService.GetAllRoles();

            foreach (var temp in rolesList)
            {
                ls.Add(new SelectListItem() { Text = temp.Name, Value = temp.Id.ToString() });
            }
            return ls;
        }

        [HttpPost]
        public ActionResult AddUser(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_userService.GetUserByEmail(addUserViewModel.Email) == null)
                    {
                        try
                        {
                            _userService.CreateUserAccountAdmin(addUserViewModel.Email, addUserViewModel.Password, addUserViewModel.FirstName, addUserViewModel.LastName, addUserViewModel.RoleId);
                            return RedirectToAction("MenageUsers", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie użytkownika.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Użytkownik o takim Emailu już istnieje.");
                    }
                }
            }
            ViewBag.Roles = GetRoles();
            return View();
        }

        public ActionResult EditUser(int id)
        {
            {
                User user = _userService.GetUserById(id);
                EditUserViewModel editUserViewModel = new EditUserViewModel
                {
                    Email = _userService.GetUserById(id).Email,
                    LockoutEnabled = _userService.GetUserById(id).LockoutEnabled,
                    RoleId = _userService.GetUserById(id).RoleId
                };
                ViewBag.Roles = GetRoles();
                return View(editUserViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    User user = _userService.GetUserByEmail(editUserViewModel.Email);

                    if (user != null)
                    {
                        if (!editUserViewModel.Email.Equals(User.Identity.Name))
                        {
                            try
                            {
                                _userService.EditUserAccountAdmin(user.Email, editUserViewModel.LockoutEnabled, editUserViewModel.RoleId);
                                return RedirectToAction("MenageUsers", "Admin");
                            }
                            catch
                            {
                                ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie użytkownika.");
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("", "Nie możesz edytować swoich danych z własnego konta administratora.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Użytkownik o tej nazwie nie istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteUser(int id)
        {
            {
                DeleteUserViewModel deleteUserViewModel = new DeleteUserViewModel
                {
                    Email = _userService.GetUserById(id).Email
                };
                return View(deleteUserViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(DeleteUserViewModel deleteUserViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (!deleteUserViewModel.Email.Equals(User.Identity.Name))
                    {
                        try
                        {
                            _userService.DeleteUser(deleteUserViewModel.Email);
                            return RedirectToAction("MenageUsers", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić usunięcie użytkownika.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Nie możesz usunąć samego siebie.");
                    }
                }
            }
            return View();
        }
        #endregion

        #region Course types
        public ActionResult MenageCourseTypes()
        {
            List<CourseType> list = _courseTypeService.GetAllCourseTypes();
            return View(list);
        }

        public ActionResult AddCourseType()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourseType(AddCourseTypeViewModel addCourseTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_courseTypeService.GetCourseTypeByName(addCourseTypeViewModel.Name) == null)
                    {
                        try
                        {
                            _courseTypeService.CreateCourseType(addCourseTypeViewModel.Name);
                            return RedirectToAction("MenageCourseTypes", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie typu.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Typ kursu o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditCourseType(int id)
        {
            {
                CourseType courseType = _courseTypeService.GetCourseTypeById(id);
                EditCourseTypeViewModel editCourseTypeViewModel = new EditCourseTypeViewModel
                {
                    Name = _courseTypeService.GetCourseTypeById(id).Name
                };
                return View(editCourseTypeViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditCourseType(EditCourseTypeViewModel editCourseTypeViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    CourseType courseType = _courseTypeService.GetCourseTypeByName(editCourseTypeViewModel.Name);

                    if (courseType != null && courseType.Id != editCourseTypeViewModel.Id)
                    {
                        ModelState.AddModelError("", "Typ kursu o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _courseTypeService.SetCourseTypeName(editCourseTypeViewModel.Id, editCourseTypeViewModel.Name);
                        return RedirectToAction("MenageCourseTypes", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie typu.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteCourseType(int id)
        {
            {
                DeleteCourseTypeViewModel deleteCourseTypeViewModel = new DeleteCourseTypeViewModel
                {
                    Name = _courseTypeService.GetCourseTypeById(id).Name
                };
                return View(deleteCourseTypeViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteCourseType(DeleteCourseTypeViewModel deleteCourseTypeViewModel)
        {
            {
                _courseTypeService.DeleteCourseType(deleteCourseTypeViewModel.Name);
                return RedirectToAction("MenageCourseTypes", "Admin");
            }
        }
        #endregion

        #region Course kinds
        public ActionResult MenageCourseKinds()
        {
            List<CourseKind> list = _courseKindService.GetAllCourseKinds();
            return View(list);
        }

        public ActionResult AddCourseKind()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCourseKind(AddCourseKindViewModel addCourseKindViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_courseKindService.GetCourseKindByName(addCourseKindViewModel.Name) == null)
                    {
                        try
                        {
                            _courseKindService.CreateCourseKind(addCourseKindViewModel.Name);
                            return RedirectToAction("MenageCourseKinds", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie rodzaju.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Rodzaj kursu o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditCourseKind(int id)
        {
            {
                CourseKind courseKind = _courseKindService.GetCourseKindById(id);
                EditCourseKindViewModel editCourseKindViewModel = new EditCourseKindViewModel
                {
                    Name = _courseKindService.GetCourseKindById(id).Name
                };
                return View(editCourseKindViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditCourseKind(EditCourseKindViewModel editCourseKindViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    CourseKind courseKind = _courseKindService.GetCourseKindByName(editCourseKindViewModel.Name);

                    if (courseKind != null && courseKind.Id != editCourseKindViewModel.Id)
                    {
                        ModelState.AddModelError("", "Rodzaj kursu o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _courseKindService.SetCourseKindName(editCourseKindViewModel.Id, editCourseKindViewModel.Name);
                        return RedirectToAction("MenageCourseKinds", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie rodzaju.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteCourseKind(int id)
        {
            {
                DeleteCourseKindViewModel deleteCourseKindViewModel = new DeleteCourseKindViewModel
                {
                    Name = _courseKindService.GetCourseKindById(id).Name
                };
                return View(deleteCourseKindViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteCourseKind(DeleteCourseKindViewModel deleteCourseKindViewModel)
        {

            {
                _courseKindService.DeleteCourseKind(deleteCourseKindViewModel.Name);
                return RedirectToAction("MenageCourseKinds", "Admin");
            }
        }
        #endregion

        #region Foreign languages
        public ActionResult MenageForeignLanguages()
        {
            List<ForeignLanguage> list = _foreignLanguageService.GetAllForeignLanguages();
            return View(list);
        }

        public ActionResult AddForeignLanguage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddForeignLanguage(AddForeignLanguageViewModel addForeignLanguageViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_foreignLanguageService.GetForeignLanguageByName(addForeignLanguageViewModel.Name) == null)
                    {
                        try
                        {
                            _foreignLanguageService.CreateForeignLanguage(addForeignLanguageViewModel.Name);
                            return RedirectToAction("MenageForeignLanguages", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie języka.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Język obcy o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditForeignLanguage(int id)
        {
            {
                ForeignLanguage foreignLanguage = _foreignLanguageService.GetForeignLanguageById(id);
                EditForeignLanguageViewModel editForeignLanguageViewModel = new EditForeignLanguageViewModel
                {
                    Name = _foreignLanguageService.GetForeignLanguageById(id).Name
                };
                return View(editForeignLanguageViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditForeignLanguage(EditForeignLanguageViewModel editForeignLanguageViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    ForeignLanguage foreignLanguage = _foreignLanguageService.GetForeignLanguageByName(editForeignLanguageViewModel.Name);

                    if (foreignLanguage != null && foreignLanguage.Id != editForeignLanguageViewModel.Id)
                    {
                        ModelState.AddModelError("", "Język obcy o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _foreignLanguageService.SetForeignLanguageName(editForeignLanguageViewModel.Id, editForeignLanguageViewModel.Name);
                        return RedirectToAction("MenageForeignLanguages", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie języka.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteForeignLanguage(int id)
        {
            {
                DeleteForeignLanguageViewModel deleteForeignLanguageViewModel = new DeleteForeignLanguageViewModel
                {
                    Name = _foreignLanguageService.GetForeignLanguageById(id).Name
                };
                return View(deleteForeignLanguageViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteForeignLanguage(DeleteForeignLanguageViewModel deleteForeignLanguageViewModel)
        {
            {
                _foreignLanguageService.DeleteForeignLanguage(deleteForeignLanguageViewModel.Name);
                return RedirectToAction("MenageForeignLanguages", "Admin");
            }
        }
        #endregion

        #region Foreign languages levels
        public ActionResult MenageForeignLanguageLevels()
        {
            List<ForeignLanguageLevel> list = _foreignLanguageLevelService.GetAllForeignLanguageLevels();
            return View(list);
        }

        public ActionResult AddForeignLanguageLevel()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddForeignLanguageLevel(AddForeignLanguageLevelViewModel addForeignLanguageLevelViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_foreignLanguageLevelService.GetForeignLanguageLevelByName(addForeignLanguageLevelViewModel.Name) == null)
                    {
                        try
                        {
                            _foreignLanguageLevelService.CreateForeignLanguageLevel(addForeignLanguageLevelViewModel.Name);
                            return RedirectToAction("MenageForeignLanguageLevels", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie poziomu.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Poziom znajomości języka obcego o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditForeignLanguageLevel(int id)
        {
            {
                ForeignLanguageLevel foreignLanguageLevel = _foreignLanguageLevelService.GetForeignLanguageLevelById(id);
                EditForeignLanguageLevelViewModel editForeignLanguageLevelViewModel = new EditForeignLanguageLevelViewModel
                {
                    Name = _foreignLanguageLevelService.GetForeignLanguageLevelById(id).Name
                };
                return View(editForeignLanguageLevelViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditForeignLanguageLevel(EditForeignLanguageLevelViewModel editForeignLanguageLevelViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    ForeignLanguageLevel foreignLanguageLevel = _foreignLanguageLevelService.GetForeignLanguageLevelByName(editForeignLanguageLevelViewModel.Name);

                    if (foreignLanguageLevel != null && foreignLanguageLevel.Id != editForeignLanguageLevelViewModel.Id)
                    {
                        ModelState.AddModelError("", "Poziom znajomości jezyka o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _foreignLanguageLevelService.SetForeignLanguageLevelName(editForeignLanguageLevelViewModel.Id, editForeignLanguageLevelViewModel.Name);
                        return RedirectToAction("MenageForeignLanguageLevels", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie poziomu.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteForeignLanguageLevel(int id)
        {
            {
                DeleteForeignLanguageLevelViewModel deleteForeignLanguageLevelViewModel = new DeleteForeignLanguageLevelViewModel
                {
                    Name = _foreignLanguageLevelService.GetForeignLanguageLevelById(id).Name
                };
                return View(deleteForeignLanguageLevelViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteForeignLanguageLevel(DeleteForeignLanguageLevelViewModel deleteForeignLanguageLevelViewModel)
        {
            {
                _foreignLanguageLevelService.DeleteForeignLanguageLevel(deleteForeignLanguageLevelViewModel.Name);
                return RedirectToAction("MenageForeignLanguageLevels", "Admin");
            }
        }
        #endregion

        #region Driving licenses
        public ActionResult MenageDrivingLicenses()
        {
            List<DrivingLicense> list = _drivingLicenseService.GetAllDrivingLicenses();
            return View(list);
        }

        public ActionResult AddDrivingLicense()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDrivingLicense(AddDrivingLicenseViewModel addDrivingLicenseViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    if (_drivingLicenseService.GetDrivingLicenseByCategory(addDrivingLicenseViewModel.Category) == null)
                    {
                        try
                        {
                            _drivingLicenseService.CreateDrivingLicense(addDrivingLicenseViewModel.Category);
                            return RedirectToAction("MenageDrivingLicenses", "Admin");
                        }
                        catch
                        {
                            ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie kategorii.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Kategoria o tej nazwie już istnieje.");
                    }
                }
            }
            return View();
        }

        public ActionResult EditDrivingLicense(int id)
        {
            {
                DrivingLicense drivingLicense = _drivingLicenseService.GetDrivingLicenseById(id);
                EditDrivingLicenseViewModel editDrivingLicenseViewModel = new EditDrivingLicenseViewModel
                {
                    Category = _drivingLicenseService.GetDrivingLicenseById(id).Category
                };
                return View(editDrivingLicenseViewModel);
            }
        }

        [HttpPost]
        public ActionResult EditDrivingLicense(EditDrivingLicenseViewModel editDrivingLicenseViewModel)
        {
            if (ModelState.IsValid)
            {
                {
                    DrivingLicense drivingLicense = _drivingLicenseService.GetDrivingLicenseByCategory(editDrivingLicenseViewModel.Category);

                    if (drivingLicense != null && drivingLicense.Id != editDrivingLicenseViewModel.Id)
                    {
                        ModelState.AddModelError("", "Kategoria o tej nazwie już istnieje.");
                        return View();
                    }

                    try
                    {
                        _drivingLicenseService.SetDrivingLicenseCategory(editDrivingLicenseViewModel.Id, editDrivingLicenseViewModel.Category);
                        return RedirectToAction("MenageDrivingLicenses", "Admin");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie kategorii.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteDrivingLicense(int id)
        {
            {
                DeleteDrivingLicenseViewModel deleteDrivingLicenseViewModel = new DeleteDrivingLicenseViewModel
                {
                    Category = _drivingLicenseService.GetDrivingLicenseById(id).Category
                };
                return View(deleteDrivingLicenseViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteDrivingLicense(DeleteDrivingLicenseViewModel deleteDrivingLicenseViewModel)
        {
            {
                _drivingLicenseService.DeleteDrivingLicense(deleteDrivingLicenseViewModel.Category);
                return RedirectToAction("MenageDrivingLicenses", "Admin");
            }
        }
        #endregion
    }
}