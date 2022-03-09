using System.Collections.Generic;
using RP.Entities.User;

namespace RP.AdminModule.Interfaces
{
    public interface IForeignLanguageLevelService
    {
        void CreateForeignLanguageLevel(string foreignLanguageLevelName);
        void DeleteForeignLanguageLevel(string foreignLanguageLevelName);
        List<ForeignLanguageLevel> GetAllForeignLanguageLevels();
        ForeignLanguageLevel GetForeignLanguageLevelById(int foreignLanguageLevelId);
        ForeignLanguageLevel GetForeignLanguageLevelByName(string foreignLanguageLevelName);
        string GetForeignLanguageLevelName(int foreignLanguageLevelId);
        void SetForeignLanguageLevelName(int foreignLanguageLevelId, string foreignLanguageLevelName);
    }
}