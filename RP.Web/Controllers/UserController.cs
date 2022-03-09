using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using RP.Entities.UserModule.ViewModels;
using RP.Data.Context;
using RP.Entities.User;
using Rotativa;
using System.Data.SqlClient;
using System.Data;
using RP.AccountModule.Services;
using RP.Entities.Recruitment;

namespace RP.Controllers
{
    [Authorize(Roles = "Użytkownik")]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index(List<RecruitmentProcess> filteredRecruitmentProcessesList2)
        {
            if (filteredRecruitmentProcessesList2 == null)
            {
                using (RPDbContext db = new RPDbContext())
                {
                    string userName = User.Identity.Name;
                    UserModule.UserModule um = new UserModule.UserModule();
                    int id = um.GetUserId(userName);
                    List<string> skills = db.Skills.Where(s => s.UserId == id).Select(n => n.Name).ToList();
                    var splitted =  um.SplitintoSingle(skills);

                    filteredRecruitmentProcessesList2 = um.FilterProcessesBySkills(splitted);
                        
                    return View(filteredRecruitmentProcessesList2);
                }
            }
            return View(filteredRecruitmentProcessesList2);
        }

        public ActionResult EditProfile(UserProfileViewModel edv) // sam odczyt
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                um.UpdateUserBasicData(edv, userName, db, "r");
            }
            return View(edv);
        }

        [HttpPost] // aktualizacja danych po klikniecu "Save"
        public ActionResult EditProfile(UserProfileViewModel edv, string FirstName)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                um.UpdateUserBasicData(edv, userName, db, "u");
                ModelState.AddModelError("", "Zaktualizowano dane.");
            }
            return View();
        }

        public ActionResult EditEducationData(EducationDataViewModel edvm)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserService userService = new UserService(db);
                UserModule.UserModule um = new UserModule.UserModule();
                string action = "";
                edvm.EducationLevelName = um.GetEducationLevels(userService.GetUserByEmail(userName).Id, db);
                um.GetEducations(edvm, db, userName);
                um.EditUserEducationData(edvm, userName, db, "", action);
            }
            return View(edvm);
        }

        [HttpPost]
        public ActionResult EditEducationData(EducationDataViewModel edvm, string action)
        {
            using (RPDbContext db = new RPDbContext())
            {

                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                if (action.Equals("edit"))
                {
                    um.EditUserEducationData(edvm, userName, db, "", action);
                    return RedirectToAction("EditEducationData", new { selectedId = edvm.selectedId,  NameOfSchool = edvm.NameOfSchool, Major = edvm.Major, Minor = edvm.Minor });
                }
                if (action.Equals("delete"))
                {
                    um.EditUserEducationData(edvm, userName, db, "w", action);
                    return View();
                }
                um.EditUserEducationData(edvm, userName, db, "w", action);
               // ModelState.AddModelError("", "Zaktualizowano dane.");
                
                
            }
            return RedirectToAction("EditEducationData", new { selectedId = edvm.selectedId, NameOfSchool = edvm.NameOfSchool, Major = edvm.Major, Minor = edvm.Minor });
        }

        public ActionResult EditCourses(CoursesViewModel cvm)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                um.getCourses(cvm, userName); // pobiera elementy do wszystkich trzech list
                string action = "";
                um.EditCoursesData(cvm, userName, db, "", action);
            }
            return View(cvm);
        }

        [HttpPost]
        public ActionResult EditCourses(CoursesViewModel cvm, string action)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                if (action.Equals("edit"))
                {
                    um.EditCoursesData(cvm, userName, db, "r", action);
                    return RedirectToAction("EditCourses", new {selectedId = cvm.selectedId, CourseKindId = cvm.CourseKindId, CourseTypeId = cvm.CourseTypeId, DateFrom = cvm.DateFrom, DateTo = cvm.DateTo, Name = cvm.Name});
                    // return View(cvm);
                }

                um.EditCoursesData(cvm, userName, db, "w", action);
                ModelState.AddModelError("", "Zaktualizowano dane.");
            }

            return RedirectToAction("EditCourses", new { selectedId = cvm.selectedId, CourseKindId = cvm.CourseKindId, CourseTypeId = cvm.CourseTypeId, DateFrom = cvm.DateFrom, DateTo = cvm.DateTo, Name = cvm.Name });
        }

        public ActionResult EditForeignLanguages(ForeignLanguagesViewModel flaviw)
        {
            using (RPDbContext db = new RPDbContext())
            {
                UserModule.UserModule um = new UserModule.UserModule();
                string userName = User.Identity.Name;
                um.getLanguages(flaviw, db,userName);
                um.editForeignLanguages(flaviw, userName, db, "", "edit");
            }
           
            return View(flaviw);
        }

        [HttpPost]
        public ActionResult EditForeignLanguages(ForeignLanguagesViewModel flaviw, string action)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                if (action.Equals("edit"))
                {
                    um.editForeignLanguages(flaviw, userName, db, "", action);
                    return RedirectToAction("EditForeignLanguages", new { selectedId = flaviw.selectedId, ForeignLanguageId = flaviw.ForeginLanguageId, LanguageLevelId = flaviw.ForeignLangauegeLevelId, action = "edit" });
                }
                if (action.Equals("add"))
                {
                    um.editForeignLanguages(flaviw, userName, db, "", "add");
                    return RedirectToAction("EditForeignLanguages", new { selectedId = flaviw.selectedId, ForeignLanguageId = flaviw.ForeginLanguageId, LanguageLevelId = flaviw.ForeignLangauegeLevelId });
                }
                if (action.Equals("delete"))
                {
                    um.editForeignLanguages(flaviw, userName, db, "", "delete");

                }

                um.editForeignLanguages(flaviw, userName, db, "", "Save");
            }

            return RedirectToAction("EditForeignLanguages", new { selectedId = flaviw.selectedId, ForeignLanguageId = flaviw.ForeginLanguageId, LanguageLevelId = flaviw.ForeignLangauegeLevelId });
        }

        public ActionResult CV(cvViewModel cvView)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserService ur = new UserService(db);
                UserModule.UserModule um = new UserModule.UserModule();
                int id = um.GetUserId(userName);
                var account = db.PersonalData.Find(id);
                var user = ur.GetUserByEmail(userName);
                cvView.Name = account.FirstName + " " + account.LastName;
                cvView.Birthday = um.getDateFromDataTime(account.Birthday);
                cvView.City = account.City;
                cvView.Phone = account.Phone;
                cvView.education = user.Education.ToList();
                cvView.courses = user.Courses.ToList();
                cvView.languages = user.UserForeignLanguages.ToList();
                cvView.skills = user.Skills.ToList();
                cvView.workexp = user.WorkExperience.ToList();
            }
            return new ViewAsPdf(cvView) { FileName = String.Format("cv.pdf") };
        }

        public ActionResult EditInterests(InterestsViewModel ivm)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                UserService ur = new UserService(db);
                int id = um.GetUserId(userName);
                var account = db.PersonalData.Find(id);
                var user = ur.GetUserByEmail(userName);

                ivm.list = user.Interests.ToList();
                um.editInterests(ivm, userName, db, "edit");
            }

            return View(ivm);
        }

        [HttpPost]
        public ActionResult EditInterests(InterestsViewModel ivm, string action)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                UserService ur = new UserService(db);
                int id = um.GetUserId(userName);
                var account = db.PersonalData.Find(id);
                var user = ur.GetUserByEmail(userName);

                ivm.list = user.Interests.ToList();

                if (action.Equals("edit"))
                {
                    um.editInterests(ivm, userName, db, action);
                    return RedirectToAction("EditInterests", new { Name = ivm.Name , Description = ivm.Description, selectedId = ivm.selectedId });
                }
                if (action.Equals("add"))
                {
                    um.editInterests(ivm, userName, db, action);
                    return RedirectToAction("EditInterests", new {  });
                }
                if (action.Equals("delete"))
                {
                    um.editInterests(ivm, userName, db, action);
                }

                um.editInterests(ivm, userName, db, "Save");
            }

            return RedirectToAction("EditInterests", new {  });
        }


        public ActionResult EditSkills(SkillsViewModel svm)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                UserService ur = new UserService(db);
                int id = um.GetUserId(userName);
                var account = db.PersonalData.Find(id);
                var user = ur.GetUserByEmail(userName);

                svm.listSkills = user.Skills.ToList();
                um.editSkills(svm, userName, db, "edit");
            }

            return View(svm);
        }

        [HttpPost]
        public ActionResult EditSkills(SkillsViewModel svm, string action)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                UserService ur = new UserService(db);
                int id = um.GetUserId(userName);
                var account = db.PersonalData.Find(id);
                var user = ur.GetUserByEmail(userName);

                svm.listSkills = user.Skills.ToList();

                if (action.Equals("edit"))
                {
                    um.editSkills(svm, userName, db, action);
                    return RedirectToAction("EditSkills", new { Name = svm.Name, Description = svm.Description, selectedId = svm.selectedId });
                }
                if (action.Equals("add"))
                {
                    um.editSkills(svm, userName, db, action);
                    return RedirectToAction("EditSkills", new { });
                }
                if (action.Equals("delete"))
                {
                    um.editSkills(svm, userName, db, action);
                }

                um.editSkills(svm, userName, db, "Save");
            }

            return RedirectToAction("EditSkills", new { });
        }

        public string GetCategory(int Id)
        {
            using (RPDbContext db = new RPDbContext())
            {
                List<string> lista = db.DrivingLicenses.Where(d => d.Id == Id).Select(d => d.Category).ToList();
                string Category = lista.ElementAt(0);
                return Category;
            }
        }


        public ActionResult EditDrivingLicenses(DrivingLivensesViewModel drivlic)
        {
            using (RPDbContext db = new RPDbContext())
            {
                SqlConnection sqlConnection1 = new SqlConnection("Data Source=DESKTOP-LA7EGSL;Initial Catalog=PortalRekrutacja;Integrated Security=True");
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                drivlic.catList = db.DrivingLicenses.ToList();
                UserModule.UserModule um = new RP.UserModule.UserModule();
                //    db.DrivingLicenses.Where(d => d.Users.Where(u => u.Id == )
                cmd.CommandText = "SELECT * FROM DrivingLicenseUsers Where User_Id= " + um.GetUserId(User.Identity.Name);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;


                sqlConnection1.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    DrivingLicense lic = new DrivingLicense();
                    lic.Id = reader.GetInt32(0); // tu jest ID kategorii
                    int Cat = reader.GetInt32(1); // id usera 
                    lic.Category = GetCategory(lic.Id);                    
                    drivlic.list.Add(lic);
                }

                sqlConnection1.Close();


                return View(drivlic);
            }
        }

        [HttpPost]
        public ActionResult EditDrivingLicenses(DrivingLivensesViewModel drivlic, string action)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;
                UserModule.UserModule um = new UserModule.UserModule();
                UserService ur = new UserService(db);
                int id = um.GetUserId(userName);
                var account = db.PersonalData.Find(id);
                var user = ur.GetUserByEmail(userName);

                if (action.Equals("add"))
                {
                    um.editDrivingLicenses(drivlic, userName, db, action);
                    return View(drivlic);
                }
                if (action.Equals("delete"))
                {
                    um.editDrivingLicenses(drivlic, userName, db, action);
                    return View(drivlic);

                }
                if (action.Equals("edit"))
                {
                    um.editDrivingLicenses(drivlic, userName, db, action);
                  //  return View(drivlic);
                     return RedirectToAction("EditDrivingLicenses", new { list = drivlic.list, catList = drivlic.catList, selectedId = drivlic.selectedId, selectedCatId = drivlic.selectedCatId });
                }
                if (action.Equals("EditDrivingLicenses"))
                {
                    um.editDrivingLicenses(drivlic, userName, db, action);
                    return View(drivlic);
                }

            }
            return View();
        }

        public ActionResult WorkExperience()
        {
            RPDbContext db = new RPDbContext();
            UserModule.UserModule um = new UserModule.UserModule();
            int id = um.GetUserId(User.Identity.Name);
            var list = db.WorkExperience.Where(n => n.UserId == id).AsEnumerable();
            return View(list);
        }

        public ActionResult EditWorkExperience(int id)
        {
            using (RPDbContext db = new RPDbContext())
            {
                string userName = User.Identity.Name;

                UserModule.UserModule um = new UserModule.UserModule();
                UserService ur = new UserService(db);

                
                var user = ur.GetUserByEmail(userName);

                EditWorkExperienceViewModel editWork = new EditWorkExperienceViewModel
                {
                    WorkExperienceID = id,
                    Position = user.WorkExperience.Where(n => n.Id == id).Single().Position,
                    DateFrom = user.WorkExperience.Where(n => n.Id == id).Single().DateFrom.Value,
                    DateTo = user.WorkExperience.Where(n => n.Id == id).Single().DateTo.Value,
                    Responsibilities = user.WorkExperience.Where(n => n.Id == id).Single().Responsibilities,
                    Company = user.WorkExperience.Where(n => n.Id == id).Single().Company
                };

                return View(editWork);
            }
        }
            
        [HttpPost]
        public ActionResult EditWorkExperience(EditWorkExperienceViewModel editWork, int id)
        {
            if (ModelState.IsValid)
            {
                using (RPDbContext db = new RPDbContext())
                {
                    UserModule.UserModule um = new UserModule.UserModule();
                    UserService ur = new UserService(db);
                    var user = ur.GetUserByEmail(User.Identity.Name);

                    try
                    {
                        var workexp = user.WorkExperience.FirstOrDefault(n => n.Id == id);
                        if (workexp != null)
                        {
                            workexp.Position = editWork.Position;
                            workexp.Responsibilities = editWork.Responsibilities;
                            workexp.DateFrom = editWork.DateFrom;
                            workexp.DateTo = editWork.DateTo;
                            workexp.Company = editWork.Company;
                            db.SaveChanges();
                            return RedirectToAction("WorkExperience", "User");
                        }
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić edytowanie Doświadczenia zawodowego.");
                    }
                }
            }
            return View();
        }

        public ActionResult AddWorkExperience()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddWorkExperience(AddWorkExperienceViewModel addWorkExp)
        {
            if (ModelState.IsValid)
            {
                using (RPDbContext db = new RPDbContext())
                {
                    UserModule.UserModule um = new UserModule.UserModule();
                    UserService ur = new UserService(db);
                    var user = ur.GetUserByEmail(User.Identity.Name);


                    WorkExperience workExp = new WorkExperience
                    {
                        UserId = um.GetUserId(User.Identity.Name),
                        Position = addWorkExp.Position,
                        Responsibilities = addWorkExp.Responsibilities,
                        DateFrom = addWorkExp.DateFrom,
                        DateTo = addWorkExp.DateTo,
                        Company = addWorkExp.Company
                    };

                    try
                    {
                        user.WorkExperience.Add(workExp);
                        db.SaveChanges();
                        return RedirectToAction("WorkExperience", "User");
                    }
                    catch
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę ponowić dodawanie Doświadczenia zawodowego.");
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteWorkExperience(int id)
        {
            using (RPDbContext db = new RPDbContext())
            {
                UserModule.UserModule um = new UserModule.UserModule();
                UserService ur = new UserService(db);
                var user = ur.GetUserByEmail(User.Identity.Name);


                DeleteWorkExperienceViewModel deleteWorkExperienceViewModel = new DeleteWorkExperienceViewModel
                {
                    Position = user.WorkExperience.Where(n => n.Id == id).Select(n => n.Position).Single()
                };
                return View(deleteWorkExperienceViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteWorkExperience(DeleteWorkExperienceViewModel deleteUserViewModel, int id)
        {
            if (ModelState.IsValid)
            {
                using (RPDbContext db = new RPDbContext())
                {
                    UserModule.UserModule um = new UserModule.UserModule();
                    UserService ur = new UserService(db);
                    var user = ur.GetUserByEmail(User.Identity.Name);

                    try
                    {
                        SqlConnection sqlConnection1 = new SqlConnection("Data Source=tcp:mssql01.dcsweb.pl,51433;Initial Catalog=1778_recruitmentportal;User ID=1778_recruitm;Password=9ab9qoga@Ju");
                        var item = user.WorkExperience.SingleOrDefault(n => n.Id == id);
                        if (item != null)
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = "DELETE FROM WorkExperiences WHERE Id =" + id;
                            cmd.CommandType = CommandType.Text;
                            cmd.Connection = sqlConnection1;
                            sqlConnection1.Open();
                            cmd.ExecuteNonQuery();

                            sqlConnection1.Close();

                            
                            db.SaveChanges();
                        }
                        
                        return RedirectToAction("WorkExperience", "User");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Wystąpiły błędy, proszę spróbować ponownie. " + e.ToString());
                    }
                }
            }
            return View();
        }

        public ActionResult DeleteUserAccount()
        {
            using (RPDbContext db = new RPDbContext())
            {
                UserService userService = new UserService(db);
                UserModule.UserModule um = new UserModule.UserModule();
                DeleteUserAccountViewModel deleteUserViewModel = new DeleteUserAccountViewModel
                {
                    Email = User.Identity.Name
                };
                return View(deleteUserViewModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteUserAccount(DeleteUserAccountViewModel deleteUser)
        {
            if (ModelState.IsValid)
            {
                using (RPDbContext db = new RPDbContext())
                {
                    UserService userService = new UserService(db);
                    var user = userService.GetUserByEmail(User.Identity.Name);

                    if (deleteUser.Email.Equals(User.Identity.Name))
                    {
                        try
                        {
                            userService.DeleteUser(deleteUser.Email);
                            Session.Abandon();
                            return RedirectToAction("Index", "Home");
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

        public ActionResult Activation()
        {
            return View();
        }
    }
}