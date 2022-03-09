using System.Collections.Generic;
using RP.Entities.Recruitment;

namespace RP.AdminModule.Interfaces
{
    public interface ICompanyService
    {
        void CreateCompany(string companyName);
        void DeleteCompany(string companyName);
        List<Company> GetAllCompanies();
        Company GetCompanyById(int companyId);
        Company GetCompanyByName(string companyName);
        string GetCompanyName(int companyId);
        void SetCompanyName(int companyId, string companyName);
    }
}