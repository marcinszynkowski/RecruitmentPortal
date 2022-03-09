using System.Collections.Generic;
using RP.Entities.User;

namespace RP.AdminModule.Interfaces
{
    public interface IForeignLanguageService
    {
        void CreateForeignLanguage(string foreignLanguageName);
        void DeleteForeignLanguage(string foreignLanguageName);
        List<ForeignLanguage> GetAllForeignLanguages();
        ForeignLanguage GetForeignLanguageById(int foreignLanguageId);
        ForeignLanguage GetForeignLanguageByName(string foreignLanguageName);
        string GetForeignLanguageName(int foreignLanguageId);
        void SetForeignLanguageName(int foreignLanguageId, string foreignLanguageName);
    }
}