using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class ForeignLanguageLevelService : IForeignLanguageLevelService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public ForeignLanguageLevelService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public ForeignLanguageLevel GetForeignLanguageLevelByName(string foreignLanguageLevelName)
        {
            return _db.ForeignLanguageLevels.Where(fll => fll.Name == foreignLanguageLevelName).FirstOrDefault();
        }

        public ForeignLanguageLevel GetForeignLanguageLevelById(int foreignLanguageLevelId)
        {
            return _db.ForeignLanguageLevels.Find(foreignLanguageLevelId);
        }

        public string GetForeignLanguageLevelName(int foreignLanguageLevelId)
        {
            return GetForeignLanguageLevelById(foreignLanguageLevelId).Name;
        }

        public List<ForeignLanguageLevel> GetAllForeignLanguageLevels()
        {
            return _db.ForeignLanguageLevels.ToList();
        }
        #endregion

        #region Set
        public void SetForeignLanguageLevelName(int foreignLanguageLevelId, string foreignLanguageLevelName)
        {
            ForeignLanguageLevel foreignLanguageLevel = GetForeignLanguageLevelById(foreignLanguageLevelId);
            foreignLanguageLevel.Name = foreignLanguageLevelName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateForeignLanguageLevel(string foreignLanguageLevelName)
        {
            if (GetForeignLanguageLevelByName(foreignLanguageLevelName) == null)
            {
                ForeignLanguageLevel foreignLanguageLevel = new ForeignLanguageLevel
                {
                    Name = foreignLanguageLevelName,
                };
                _db.ForeignLanguageLevels.Add(foreignLanguageLevel);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteForeignLanguageLevel(string foreignLanguageLevelName)
        {
            ForeignLanguageLevel foreignLanguageLevel = GetForeignLanguageLevelByName(foreignLanguageLevelName);

            if (foreignLanguageLevel != null)
            {
                _db.ForeignLanguageLevels.Remove(foreignLanguageLevel);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}