using System;
using System.Collections.Generic;
using System.Linq;
using RP.Data.Context;
using RP.Entities.UserModule.ViewModels;
using RP.Entities.User;
using RP.Entities.Account;
using System.Data.SqlClient;
using System.Data;
using RP.AccountModule.Services;
using RP.Entities.Recruitment;

namespace RP.UserModule
{
    public class UserModule
    {
        public static List<EducationLevel> list = new List<EducationLevel>();

        public int GetUserId(string userName)
        {
            int id = 0;
            using (RPDbContext db = new RPDbContext())
            {
                List<User> list = new List<User>();
                list = db.Users.ToList();
                foreach (User user in list)
                {
                    if (user.Email.Equals(userName))
                    {
                        id = user.Id; // id usera (z personaldata)
                    }
                }

                List<PersonalData> lista = new List<PersonalData>();
                lista = db.PersonalData.ToList();
                foreach (PersonalData pdb in lista)
                {
                    if (pdb.UserId == id)
                    {
                        return pdb.UserId;
                    }
                }
            }
            return 0;
        }

        public List<RecruitmentProcess> FilterProcessesBySkills(List<string> splitted)
        {
            using (RPDbContext db = new RPDbContext())
            {
                List<RecruitmentProcess> ProcessesList = db.RecruitmentProcesses.ToList();
                List<RecruitmentProcess> recruitmentprocs = new List<RecruitmentProcess>();
                foreach (RecruitmentProcess Process in ProcessesList)
                {
                    foreach (string split in splitted)
                    {
                        if (Process.Requirements.Contains(split))
                        {
                            recruitmentprocs.Add(Process);
                        }
                    }
                }
                return recruitmentprocs;
            }
                

        }

        public List<string> SplitintoSingle(List<string> list)
        {
            List<string> splitted = new List<string>();
            
            foreach (string skill in list)
            {
               var splitted_skills = skill.Split(' ').ToList();
               
                foreach (string s in splitted_skills)
                {
                    splitted.Add(s);
                }

            }
            return splitted;
        }

        public string GetEducationLevels(int userId, RPDbContext db)
        {
            if (db.EducationLevels.Where(ui => ui.UserId == userId).FirstOrDefault() != null) 
            {
                return db.EducationLevels.Where(ui => ui.UserId == userId).FirstOrDefault().Name;
            }
            return string.Empty;
        }

        public void GetEducations(EducationDataViewModel edvm, RPDbContext db, string userName)
        {
            UserService ur = new UserService(db);

            int id = GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);

            edvm.educationList = user.Education.ToList();
        }

        // metoda do aktualizacji/odczytu podstawowych danych o uzytkowniku 
        // tryby : u - aktualizacja, r - odczyt
        public void UpdateUserBasicData(UserProfileViewModel edv, string userName, RPDbContext db, string mode)
        {
            UserService ur = new UserService(db);

            int id = GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);

            // odczyt danych i zapis do modelu
            if (mode.Equals("r"))
            {
                edv.Email = user.Email;
                edv.FirstName = account.FirstName;
                edv.LastName = account.LastName;
                edv.City = account.City;
                edv.Phone = account.Phone;
                edv.Birthday = account.Birthday.Value;
            }

            // zapis danych do bazy
            if (mode.Equals("u"))
            {
                user.Email = edv.Email;
                account.FirstName = edv.FirstName;
                account.LastName = edv.LastName;
                account.City = edv.City;
                account.Phone = edv.Phone;
                account.LastModified = DateTime.Now;
                account.Birthday = edv.Birthday;
                db.SaveChanges();
            }
        }

        public void EditUserEducationData(EducationDataViewModel edvm, string userName, RPDbContext db, string mode, string action)
        {

            UserService ur = new UserService(db);
            UserModule um = new UserModule();
            int id = um.GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);

            switch (action)
            {
                case "edit":

                    var found = db.Education.SingleOrDefault(c => c.Id == edvm.selectedId);
                    

                    GetEducationLevels(edvm.selectedId, db);
                    GetEducations(edvm, db, userName);
                    edvm.NameOfSchool = found.NameOfSchool;
                    edvm.Major = found.Major;
                    edvm.Minor = found.Minor;

                    var levelfnd = db.EducationLevels.Single(e => e.UserId == found.UserId);

                    edvm.EducationLevelName = levelfnd.Name;

                    break;
            }

            switch (mode)
            {
                
                case "w":
                    if (action.Equals("add"))
                    {
                        Education education = new Education();
                        education.UserId = user.Id;
                        education.Major = edvm.Major;
                        education.Minor = edvm.Minor;
                        education.NameOfSchool = edvm.NameOfSchool;
                        db.Education.Add(education);
                        EducationLevel level = new EducationLevel
                        {
                            Name = edvm.EducationLevelName,
                            UserId = user.Id
                        };
                        db.EducationLevels.Add(level);
                        db.SaveChanges();
                    }
                    if (action.Equals("delete"))
                    {
                        var fnd = db.Education.SingleOrDefault(c => c.Id == edvm.selectedId);
                        if (fnd != null)
                        {
                            db.Education.Remove(fnd);
                        }
                        var fndlevel = db.EducationLevels.Where(e => e.UserId == fnd.UserId).First();
                        db.EducationLevels.Remove(fndlevel);
                    }

                    var result = db.Education.SingleOrDefault(c => c.Id == edvm.selectedId);
                    if (result != null)
                    {
                      
                        result.Major = edvm.Major;
                        result.Minor = edvm.Minor;
                        result.NameOfSchool = edvm.NameOfSchool;
                        db.SaveChanges();
                        GetEducationLevels(edvm.selectedId, db);
                        GetEducations(edvm, db, userName);
                    }

                    break;
            }
        }

        public void getCourses(CoursesViewModel cvm, string userName)
        {
            using (RPDbContext db = new RPDbContext())
            {
                UserService ur = new UserService(db);
                UserModule um = new UserModule();
                int id = um.GetUserId(userName);
                var account = db.PersonalData.Find(id);
                var user = ur.GetUserByEmail(userName);

                cvm.courseList = user.Courses.ToList();
                cvm.courseTypes = db.CourseTypes.ToList();
                cvm.courseKinds = db.CourseKinds.ToList();
            }
        }

        public void EditCoursesData(CoursesViewModel cvm, string userName, RPDbContext db, string mode, string action) // add/edit course - to do : viewing all courses of the user
        {
            UserService ur = new UserService(db);
            UserModule um = new UserModule();
            int id = um.GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);
            cvm.courseList = user.Courses.ToList();
            switch (action)
            {
                case "edit":
                    var found = db.Courses.SingleOrDefault(c => c.Id == cvm.selectedId);
                    cvm.DateFrom = found.DateFrom;

                    cvm.DateTo = found.DateTo.Value.Date;
                    cvm.CourseTypeId = found.CourseTypeId;
                    cvm.CourseKindId = found.CourseKindId;
                    cvm.Name = found.Name;

                    break;
            }

            switch (mode)
            {
                case "w":
                    if (action.Equals("add")) // adding new course
                    {
                        Course course = new Course
                        {
                            CourseKindId = cvm.CourseKindId,
                            CourseTypeId = cvm.CourseTypeId,
                            DateFrom = cvm.DateFrom,
                            DateTo = cvm.DateTo,
                            Name = cvm.Name,
                            UserId = user.Id
                        };
                        db.Courses.Add(course);
                        db.SaveChanges();
                        break;
                    }
                    if (action.Equals("delete"))
                    {
                        var fnd = db.Courses.SingleOrDefault(c => c.Id == cvm.selectedId);
                        if (fnd != null)
                        {
                            db.Courses.Remove(fnd);
                        }
                    }
                    // updating selected item
                    var result = db.Courses.SingleOrDefault(c => c.Id == cvm.selectedId);
                    if (result != null)
                    {
                        result.CourseKindId = cvm.CourseTypeId;
                        result.CourseTypeId = cvm.CourseKindId;
                        result.DateFrom = cvm.DateFrom;
                        result.DateTo = cvm.DateTo;
                        result.Name = cvm.Name;
                        db.SaveChanges();
                    }
                    break;
            }
        }
        public string getDateFromDataTime(DateTime? date)
        {
            DateTime time = (DateTime)date;
            string format = "dd.MM.yyyy";
            return time.ToString(format);
        }

        public string getLangName(int id)
        {
            using (RPDbContext db = new RPDbContext())
            {
                ForeignLanguage language = db.ForeignLanguages.Find(id);
                return language.Name;
            }
        }

        public void getLanguages(ForeignLanguagesViewModel flaviw, RPDbContext db, string userName)
        {
            UserService ur = new UserService(db);
            UserModule um = new UserModule();
            int id = um.GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);

            flaviw.langList = db.ForeignLanguages.ToList();
            flaviw.langLevelsList = db.ForeignLanguageLevels.ToList();
            List<UserForeignLanguage> langl = new List<UserForeignLanguage>();
            langl = user.UserForeignLanguages.ToList();
            
            foreach (UserForeignLanguage ulang in langl)
            {
                ForeignLanguage lang = new ForeignLanguage
                {
                    Id = ulang.Id,
                    Name = getLangName(ulang.ForeignLanguageId)
                };
                flaviw.langUserLangs.Add(lang);
            }

        }

        public void editForeignLanguages(ForeignLanguagesViewModel flaviw, string userName, RPDbContext db, string mode, string action)
        {
            UserService ur = new UserService(db);
            UserModule um = new UserModule();
            int id = um.GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);
            flaviw.langLevelsList = db.ForeignLanguageLevels.ToList();
            flaviw.langList = db.ForeignLanguages.ToList();
            switch (action)
            {
                case "delete":
                    var result = db.UserForeignLanguages.SingleOrDefault(c => c.Id == flaviw.selectedId);
                    if (result != null)
                    {
                        db.UserForeignLanguages.Remove(result);
                    }
                    break;

                case "add": // adding a new element
                    UserForeignLanguage language = new UserForeignLanguage
                    {
                        UserId = user.Id,
                        ForeignLanguageId = flaviw.ForeginLanguageId,
                        ForeignLanguageLevelId = flaviw.ForeignLangauegeLevelId
                    };

                    db.UserForeignLanguages.Add(language);
                    db.SaveChanges();
                    break;

                case "edit": // read the data for editing
                    var fnd2 = db.UserForeignLanguages.SingleOrDefault(c => c.Id == flaviw.selectedId);
                    if (fnd2 != null)
                    {
                        flaviw.UserId = fnd2.UserId;
                        flaviw.ForeginLanguageId = fnd2.ForeignLanguageId;
                        flaviw.ForeignLangauegeLevelId = fnd2.ForeignLanguageLevelId;
                    }
                    break;

                case "Save": // update
                    var fnd = db.UserForeignLanguages.SingleOrDefault(c => c.Id == flaviw.selectedId);
                    if (fnd != null)
                    {
                        fnd.ForeignLanguageId = flaviw.ForeginLanguageId;
                        fnd.ForeignLanguageLevelId = flaviw.ForeignLangauegeLevelId;
                        db.SaveChanges();
                    }
                    break;
            }
        }

        public void editInterests(InterestsViewModel ivm, string userName, RPDbContext db, string action)
        {
            UserService ur = new UserService(db);
            UserModule um = new UserModule();
            int id = um.GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);

            switch (action)
            {
                case "delete":
                    var result = db.Interest.SingleOrDefault(c => c.Id == ivm.selectedId);
                    if (result != null)
                    {
                        db.Interest.Remove(result);
                    }
                    break;

                case "add": // adding a new element
                    Interest interest = new Interest
                    {
                        UserId = user.Id,
                        Name = ivm.Name,
                        Description = ivm.Description
                    };

                    db.Interest.Add(interest);
                    db.SaveChanges();
                    break;

                case "edit": // read the data for editing
                    var fnd2 = db.Interest.SingleOrDefault(c => c.Id == ivm.selectedId);
                    if (fnd2 != null)
                    {
                        ivm.UserId = fnd2.UserId;
                        ivm.Name = fnd2.Name;
                        ivm.Description = fnd2.Description;
                    }
                    break;

                case "Save": // update
                    var fnd = db.Interest.SingleOrDefault(c => c.Id == ivm.selectedId);
                    if (fnd != null)
                    {
                        fnd.Name = ivm.Name;
                        fnd.Description = ivm.Description;
                        db.SaveChanges();
                    }
                    break;
            }
        }

        public void editSkills(SkillsViewModel svm, string userName, RPDbContext db, string action)
        {
            UserService ur = new UserService(db);
            UserModule um = new UserModule();
            int id = um.GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);

            switch (action)
            {
                case "delete":
                    var result = db.Skills.SingleOrDefault(c => c.Id == svm.selectedId);
                    if (result != null)
                    {
                        db.Skills.Remove(result);
                    }
                    db.SaveChanges();
                    break;

                case "add": // adding a new element
                    Skill skill = new Skill
                    {
                        UserId = user.Id,
                        Name = svm.Name,
                        Description = svm.Description
                    };

                    db.Skills.Add(skill);
                    db.SaveChanges();
                    break;

                case "edit": // read the data for editing
                    var fnd2 = db.Skills.SingleOrDefault(c => c.Id == svm.selectedId);
                    if (fnd2 != null)
                    {
                        svm.UserId = fnd2.UserId;
                        svm.Name = fnd2.Name;
                        svm.Description = fnd2.Description;
                    }
                    break;

                case "Save": // update
                    var fnd = db.Skills.SingleOrDefault(c => c.Id == svm.selectedId);
                    if (fnd != null)
                    {
                        fnd.Name = svm.Name;
                        fnd.Description = svm.Description;
                        db.SaveChanges();
                    }
                    break;
            }
        }

        public void editDrivingLicenses(DrivingLivensesViewModel drivlic, string userName, RPDbContext db, string action)
        {
            UserService ur = new UserService(db);
            UserModule um = new UserModule();
            int id = um.GetUserId(userName);
            var account = db.PersonalData.Find(id);
            var user = ur.GetUserByEmail(userName);

            //GetDrivingLicenseToUser(drivlic.DrivingLicense_Id)
            //dr.DeleteDrivingLicense(drivlic.selectedId);
            SqlConnection sqlConnection1 = new SqlConnection("Data Source=DESKTOP-LA7EGSL;Initial Catalog=PortalRekrutacja;Integrated Security=True");
            switch (action)
            {
                case "delete":
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM DrivingLicenseUsers WHERE DrivingLicense_Id =" + drivlic.selectedId + " AND User_ID = " + id;
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    cmd.ExecuteNonQuery();

                    sqlConnection1.Close();
                    break;
                case "add": // adding a new element
                    
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "INSERT INTO  DrivingLicenseUsers Values( " + drivlic.selectedCatId + "," + id + ")";
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    cmd2.ExecuteNonQuery();

                    sqlConnection1.Close();
                    break;
                case "edit": // read the data for editing
                    SqlCommand cmd4 = new SqlCommand();
                    cmd4.CommandText = "SELECT * FROM DrivingLicenseUsers WHERE User_Id= " + id + " ";
                    cmd4.CommandType = CommandType.Text;
                    cmd4.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    
                    SqlDataReader reader;
                    reader = cmd4.ExecuteReader();

                    while (reader.Read())
                    {
                        DrivingLicense lic = new DrivingLicense();
                        lic.Id = reader.GetInt32(0); // tu jest ID kategorii
                        int Cat = reader.GetInt32(1); // id usera 
                        List<string> lista = db.DrivingLicenses.Where(d => d.Id == lic.Id).Select(d => d.Category).ToList();
                        string Category = lista.ElementAt(0);

                        lic.Category = Category;
                        drivlic.list.Add(lic);
                    }

                    sqlConnection1.Close();
                    break;
                case "EditDrivingLicenses": // update
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.CommandText = "UPDATE DrivingLicenseUsers SET DrivingLicense_Id = " + drivlic.selectedCatId + " WHERE User_Id =" + id + " AND DrivingLicense_Id = " + drivlic.selectedId;
                    cmd3.CommandType = CommandType.Text;
                    cmd3.Connection = sqlConnection1;
                    sqlConnection1.Open();
                    cmd3.ExecuteNonQuery();

                    sqlConnection1.Close();
                    break;
            }
        }
    }
}
