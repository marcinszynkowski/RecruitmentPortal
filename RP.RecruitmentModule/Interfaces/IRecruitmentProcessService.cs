using System.Collections.Generic;
using RP.Entities.Recruitment;
using RP.Entities.RecruitmentModule.ViewModels;

namespace RP.RecruitmentModule.Interfaces
{
    public interface IRecruitmentProcessService
    {
        void CreateRecruitmentProcess(int processCode, int companyId, int cityId, int positionId, string tasks, string requirements, string offer, int? formOfEmploymentId, int? paymentTypeId, int? payment, string email, string phone);
        void DeleteRecruitmentProcess(int processCode);
        void EditRecruitmentProcess(int processId, RecruitmentProcess editedRecruitmentProcess);
        List<RecruitmentProcess> GetAllRecruitmentProcesses();
        List<RecruitmentProcess> GetFilteredRecruitmentProcessesByAmmount(int ammountFrom, int ammountTo);
        RecruitmentProcess GetRecruitmentProcessById(int processId);
        RecruitmentProcess GetRecruitmentProcessByProcessCode(int processCode);
        List<RecruitmentProcess> GetRecruitmentProcessesByCity(string city);
        List<RecruitmentProcess> GetRecruitmentProcessesByCompany(string company);
        List<RecruitmentProcess> GetRecruitmentProcessesByFormOfEmployment(string formOfEmployment);
        List<RecruitmentProcess> GetRecruitmentProcessesByPaymentType(string paymentType);
        List<RecruitmentProcess> GetRecruitmentProcessesByPosition(string position);
        List<RecruitmentProcess> GetRecruitmentProcessesByProcessStatus(int processStatusId);
        IList<RecruitmentProcessViewModel> GetFilteredRecruitmentProcesses(FilterViewModel filterViewModel);
    }
}