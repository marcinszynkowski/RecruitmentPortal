using System;
using System.Collections.Generic;
using RP.Entities.Account;
using RP.Entities.Recruitment;
using RP.Entities.User;

namespace RP.AccountModule.Interfaces
{
    public interface IUserService
    {
        void AddRecruitmentProcessToUser(string email, RecruitmentProcess recruitmentProcess);
        string CreateRandomPassword();
        void CreateUserAccount(string email, string password, string firstName, string lastName);
        void CreateUserAccountAdmin(string email, string password, string firstName, string lastName, int roleId);
        void EditUserAccountAdmin(string email, bool lockoutEnabled, int roleId);
        void DeleteUser(string email);
        int GetAccessFailedCount(string email);
        List<User> GetAllUsers();
        string GetEmail(int userId);
        DateTime? GetLastLoginDate(string email);
        bool GetLockoutEnabled(string email);
        User GetUserByEmail(string email);
        User GetUserById(int userId);
        User GetUserByVirtualCVAdress(Guid virtualCVAdress);
        EducationLevel GetUserEducationLevelById(int userId);
        PersonalData GetUserPersonalDataById(int userId);
        List<RecruitmentProcess> GetUserRecruitmentProcessesByUserEmail(string email);
        int GetUserRoleId(string email);
        void IncrementAccessFailedCount(string email);
        bool IsEmailExist(string email);
        bool IsInRole(string email, int roleId);
        void ResetAccessFailedCount(string email);
        void SetEmail(string oldEmail, string newEmail);
        void SetLastLoginDate(string email);
        void SetLockoutEnabled(string email, bool state);
        void SetPassword(string email, string password);
        void SetUserRole(string email, int roleId);
        bool ValidatePassword(string email, string password);
        void SignIn(string email);
        bool CanSignIn(string email);
        bool IsBadPassword(string email);
        void ProvidedBadPassword(string email);
        bool CanResetPassword(string email);
        string ResetPassword(string email);
        bool IsAccountNotActivated(string email);

    }
}