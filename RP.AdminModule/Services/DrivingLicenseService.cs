using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.User;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class DrivingLicenseService : IDrivingLicenseService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public DrivingLicenseService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public DrivingLicense GetDrivingLicenseByCategory(string drivingLicenseCategory)
        {
            return _db.DrivingLicenses.Where(dl => dl.Category == drivingLicenseCategory).FirstOrDefault();
        }

        public DrivingLicense GetDrivingLicenseById(int drivingLicenseId)
        {
            return _db.DrivingLicenses.Find(drivingLicenseId);
        }

        public string GetDrivingLicenseCategory(int drivingLicenseId)
        {
            return GetDrivingLicenseById(drivingLicenseId).Category;
        }

        public List<DrivingLicense> GetAllDrivingLicenses()
        {
            return _db.DrivingLicenses.ToList();
        }
        #endregion

        #region Set
        public void SetDrivingLicenseCategory(int drivingLicenseId, string drivingLicenseCategory)
        {
            DrivingLicense drivingLicense = GetDrivingLicenseById(drivingLicenseId);
            drivingLicense.Category = drivingLicenseCategory;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateDrivingLicense(string drivingLicenseCategory)
        {
            if (GetDrivingLicenseByCategory(drivingLicenseCategory) == null)
            {
                DrivingLicense drivingLicense = new DrivingLicense
                {
                    Category = drivingLicenseCategory,
                };
                _db.DrivingLicenses.Add(drivingLicense);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteDrivingLicense(string drivingLicenseCategory)
        {
            DrivingLicense drivingLicense = GetDrivingLicenseByCategory(drivingLicenseCategory);

            if (drivingLicense != null)
            {
                _db.DrivingLicenses.Remove(drivingLicense);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}