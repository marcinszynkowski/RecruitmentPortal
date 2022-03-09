using System.Collections.Generic;
using RP.Entities.User;

namespace RP.AdminModule.Interfaces
{
    public interface IDrivingLicenseService
    {
        void CreateDrivingLicense(string drivingLicenseCategory);
        void DeleteDrivingLicense(string drivingLicenseCategory);
        List<DrivingLicense> GetAllDrivingLicenses();
        DrivingLicense GetDrivingLicenseByCategory(string drivingLicenseCategory);
        DrivingLicense GetDrivingLicenseById(int drivingLicenseId);
        string GetDrivingLicenseCategory(int drivingLicenseId);
        void SetDrivingLicenseCategory(int drivingLicenseId, string drivingLicenseCategory);
    }
}