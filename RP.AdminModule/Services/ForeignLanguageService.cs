using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class ForeignLanguageService : IForeignLanguageService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public ForeignLanguageService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public ForeignLanguage GetForeignLanguageByName(string foreignLanguageName)
        {
            return _db.ForeignLanguages.Where(fl => fl.Name == foreignLanguageName).FirstOrDefault();
        }

        public ForeignLanguage GetForeignLanguageById(int foreignLanguageId)
        {
            return _db.ForeignLanguages.Find(foreignLanguageId);
        }

        public string GetForeignLanguageName(int foreignLanguageId)
        {
            return GetForeignLanguageById(foreignLanguageId).Name;
        }

        public List<ForeignLanguage> GetAllForeignLanguages()
        {
            return _db.ForeignLanguages.ToList();
        }
        #endregion

        #region Set
        public void SetForeignLanguageName(int foreignLanguageId, string foreignLanguageName)
        {
            ForeignLanguage foreignLanguage = GetForeignLanguageById(foreignLanguageId);
            foreignLanguage.Name = foreignLanguageName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateForeignLanguage(string foreignLanguageName)
        {
            if (GetForeignLanguageByName(foreignLanguageName) == null)
            {
                ForeignLanguage foreignLanguage = new ForeignLanguage
                {
                    Name = foreignLanguageName,
                };
                _db.ForeignLanguages.Add(foreignLanguage);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteForeignLanguage(string foreignLanguageName)
        {
            ForeignLanguage foreignLanguage = GetForeignLanguageByName(foreignLanguageName);

            if (foreignLanguage != null)
            {
                _db.ForeignLanguages.Remove(foreignLanguage);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}