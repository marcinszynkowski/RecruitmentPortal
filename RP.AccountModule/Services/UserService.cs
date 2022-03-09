using RP.AccountModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Account;
using RP.Entities.Recruitment;
using RP.Entities.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Helpers;

namespace RP.AccountModule.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly RPDbContext _db;
        private readonly IRoleService _roleService;
        #endregion

        #region Constructor
        public UserService(RPDbContext db)
        {
            _db = db;
        }

        public UserService(RPDbContext db, IRoleService roleService)
        {
            _db = db;
            _roleService = roleService;
        }
        #endregion

        #region Get
        public int GetAccessFailedCount(string email)
        {
            return GetUserByEmail(email).AccessFailedCount;
        }

        public string GetEmail(int userId)
        {
            return GetUserById(userId).Email;
        }

        public DateTime? GetLastLoginDate(string email)
        {
            return GetUserByEmail(email).LastLogin;
        }

        public bool GetLockoutEnabled(string email)
        {
            return GetUserByEmail(email).LockoutEnabled;
        }

        public int GetUserRoleId(string email)
        {
            return GetUserByEmail(email).RoleId;
        }

        public User GetUserById(int userId)
        {
            return _db.Users.Find(userId);
        }

        public User GetUserByEmail(string email)
        {
            return _db.Users.Where(u => u.Email == email).FirstOrDefault();
        }

        public User GetUserByVirtualCVAdress(Guid virtualCVAdress)
        {
            return _db.Users.Where(u => u.VirtualCVAdress == virtualCVAdress).FirstOrDefault();
        }

        public PersonalData GetUserPersonalDataById(int userId)
        {
            return _db.PersonalData.Where(pd => pd.UserId == userId).FirstOrDefault();
        }

        public EducationLevel GetUserEducationLevelById(int userId)
        {
            return _db.EducationLevels.Where(el => el.UserId == userId).FirstOrDefault();
        }

        public List<RecruitmentProcess> GetUserRecruitmentProcessesByUserEmail(string email)
        {
            User user = GetUserByEmail(email);
            List<RecruitmentProcess> recruitmentProcessList = new List<RecruitmentProcess>();
            foreach (RecruitmentProcess process in user.RecruitmentProcess)
            {
                recruitmentProcessList.Add(process);
            }
            return recruitmentProcessList;
        }

        public List<User> GetAllUsers()
        {
            return _db.Users.ToList();
        }
        #endregion

        #region Set
        public void SetEmail(string oldEmail, string newEmail)
        {
            User user = GetUserByEmail(oldEmail);
            user.Email = newEmail;
            _db.SaveChanges();
        }

        public void SetLastLoginDate(string email)
        {
            User user = GetUserByEmail(email);
            user.LastLogin = DateTime.Now;
            _db.SaveChanges();
        }

        public void SetLockoutEnabled(string email, bool state)
        {
            User user = GetUserByEmail(email);
            user.LockoutEnabled = state;
            _db.SaveChanges();
        }

        public void SetUserRole(string email, int roleId)
        {
            User user = GetUserByEmail(email);
            user.RoleId = roleId;
            _db.SaveChanges();
        }

        public void SetPassword(string email, string password)
        {
            User user = GetUserByEmail(email);
            user.Password = Crypto.HashPassword(password);
            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateUserAccount(string email, string password, string firstName, string lastName)
        {
            using (DbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    CreateUser(email, password);

                    User user = GetUserByEmail(email);

                    CreatePersonalDataForNewUser(user.Id, firstName, lastName);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public void CreateUserAccountAdmin(string email, string password, string firstName, string lastName, int roleId)
        {
            using (DbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    CreateUser(email, password);
                    SetUserRole(email, roleId);

                    User user = GetUserByEmail(email);
                    
                    CreatePersonalDataForNewUser(user.Id, firstName, lastName);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        private void CreateUser(string email, string password)
        {
            User user = new User
            {
                Email = email,
                Password = Crypto.HashPassword(password),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                RoleId = 3,
                VirtualCVAdress = Guid.NewGuid()
            };
            _db.Users.Add(user);
            _db.SaveChanges();
        }

        private void CreatePersonalDataForNewUser(int userId, string firstName, string lastName)
        {
            PersonalData personalData = new PersonalData
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName
            };

            _db.PersonalData.Add(personalData);
            _db.SaveChanges();
        }

        public string CreateRandomPassword()
        {
            string password = string.Empty;

            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890".ToCharArray();

            Random random = new Random();

            for (int i = 1; i <= 15; i++)
            {
                int letter_num = random.Next(0, letters.Length - 1);

                password += letters[letter_num];
            }

            return password;
        }
        #endregion

        #region Edit
        public void EditUserAccountAdmin(string email, bool lockoutEnabled, int roleId)
        {
            using (DbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    SetLockoutEnabled(email, lockoutEnabled);
                    if (lockoutEnabled == false)
                    {
                        ResetAccessFailedCount(email);
                    }
                    SetUserRole(email, roleId);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
        #endregion

        #region Delete
        public void DeleteUser(string email)
        {
            User user = GetUserByEmail(email);
            PersonalData personalData = GetUserPersonalDataById(user.Id);
            if (personalData != null)
            {
                _db.PersonalData.Remove(personalData);
                _db.SaveChanges();
            }
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Is
        public bool IsEmailExist(string email)
        {
            return _db.Users.Where(u => u.Email.Equals(email)).Any();
        }

        public bool IsInRole(string email, int roleId)
        {
            return GetUserByEmail(email).RoleId == roleId;
        }

        public bool ValidatePassword(string email, string password)
        {
            User user = GetUserByEmail(email);
            if (Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Reset access failed count
        public void ResetAccessFailedCount(string email)
        {
            User user = GetUserByEmail(email);
            user.AccessFailedCount = 0;
            _db.SaveChanges();
        }
        #endregion

        #region Increment access failed count
        public void IncrementAccessFailedCount(string email)
        {
            User user = GetUserByEmail(email);
            user.AccessFailedCount += 1;
            _db.SaveChanges();
        }
        #endregion

        #region Add recruitment process to user
        public void AddRecruitmentProcessToUser(string email, RecruitmentProcess recruitmentProcess)
        {
            User user = GetUserByEmail(email);
            user.RecruitmentProcess.Add(recruitmentProcess);
            _db.SaveChanges();
        }
        #endregion

        #region SignIn
        public void SignIn(string email)
        {
            using (DbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    SetLastLoginDate(email);
                    ResetAccessFailedCount(email);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }

        public bool CanSignIn(string email)
        {
            if (!GetLockoutEnabled(email) && !IsAccountNotActivated(email))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Bad Password
        public bool IsBadPassword(string email)
        {
            if (GetUserByEmail(email) != null && !GetLockoutEnabled(email))
            {
                return true;
            }
            return false;
        }

        public void ProvidedBadPassword(string email)
        {
            using (DbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    IncrementAccessFailedCount(email);
                    if (GetAccessFailedCount(email) >= 5)
                    {
                        SetLockoutEnabled(email, true);
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
        }
        #endregion

        #region Reset Password
        public bool CanResetPassword(string email)
        {
            if (IsEmailExist(email) && !IsAccountNotActivated(email))
            {
                return true;
            }
            return false;
        }

        public string ResetPassword(string email)
        {
            string password = null;
            using (DbContextTransaction transaction = _db.Database.BeginTransaction())
            {
                try
                {
                    password = CreateRandomPassword();

                    SetPassword(email, password);
                    SetLockoutEnabled(email, false);
                    ResetAccessFailedCount(email);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return password;
        }
        #endregion

        #region NotActivated
        public bool IsAccountNotActivated(string email)
        {
            if (GetUserRoleId(email) == _roleService.GetRoleByName("Nieaktywowany").Id)
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}