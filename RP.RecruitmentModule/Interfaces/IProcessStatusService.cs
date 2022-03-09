using RP.Entities.Recruitment;
using System.Collections.Generic;

namespace RP.RecruitmentModule.Interfaces
{
    public interface IProcessStatusService
    {
        void CreateProcessStatus(string processStatusName);
        void DeleteProcessStatus(string processStatusName);
        List<ProcessStatus> GetAllProcessStatus();
        ProcessStatus GetProcessStatusById(int processStatusId);
        ProcessStatus GetProcessStatusByName(string processStatusName);
        string GetProcessStatusName(int processStatusId);
        void SetProcessStatusName(int processStatusId, string processStatusName);
    }
}