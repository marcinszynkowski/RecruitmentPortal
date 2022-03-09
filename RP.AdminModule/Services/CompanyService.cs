using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Recruitment;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class CompanyService : ICompanyService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public CompanyService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public Company GetCompanyByName(string companyName)
        {
            return _db.Companies.Where(ck => ck.Name == companyName).FirstOrDefault();
        }

        public Company GetCompanyById(int companyId)
        {
            return _db.Companies.Find(companyId);
        }

        public string GetCompanyName(int companyId)
        {
            return _db.Companies.Find(companyId).Name;
        }

        public List<Company> GetAllCompanies()
        {
            return _db.Companies.ToList();
        }
        #endregion

        #region Set
        public void SetCompanyName(int companyId, string companyName)
        {
            Company company = GetCompanyById(companyId);
            company.Name = companyName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateCompany(string companyName)
        {
            if (GetCompanyByName(companyName) == null)
            {
                Company company = new Company
                {
                    Name = companyName,
                };
                _db.Companies.Add(company);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteCompany(string companyName)
        {
            Company company = GetCompanyByName(companyName);

            if (company != null)
            {
                _db.Companies.Remove(company);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}