using RP.Entities.Recruitment;
using System.Collections.Generic;

namespace RP.AdminModule.Interfaces
{
    public interface IFormOfEmploymentService
    {
        void CreateFormOfEmployment(string formOfEmploymentName);
        void DeleteFormOfEmployment(int formOfEmploymentId);
        List<FormOfEmployment> GetAllFormOfEmployment();
        FormOfEmployment GetFormOfEmploymentById(int? formOfEmploymentId);
        FormOfEmployment GetFormOfEmploymentByName(string formOfEmploymentName);
        string GetFormOfEmploymentName(int formOfEmploymentId);
        void SetFormOfEmploymentName(int? formOfEmploymentId, string formOfEmploymentName);
    }
}