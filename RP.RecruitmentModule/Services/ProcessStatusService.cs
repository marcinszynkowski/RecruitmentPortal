using RP.Data.Context;
using RP.Entities.Recruitment;
using RP.RecruitmentModule.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RP.RecruitmentModule.Services
{
    public class ProcessStatusService : IProcessStatusService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public ProcessStatusService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public ProcessStatus GetProcessStatusByName(string processStatusName)
        {
            return _db.ProcessStatus.Where(ps => ps.Name == processStatusName).FirstOrDefault();
        }

        public ProcessStatus GetProcessStatusById(int processStatusId)
        {
            return _db.ProcessStatus.Find(processStatusId);
        }

        public string GetProcessStatusName(int processStatusId)
        {
            return GetProcessStatusById(processStatusId).Name;
        }

        public List<ProcessStatus> GetAllProcessStatus()
        {
            return _db.ProcessStatus.ToList();
        }
        #endregion

        #region Set
        public void SetProcessStatusName(int processStatusId, string processStatusName)
        {
            ProcessStatus processStatus = GetProcessStatusById(processStatusId);
            processStatus.Name = processStatusName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateProcessStatus(string processStatusName)
        {
            if (GetProcessStatusByName(processStatusName) == null)
            {
                ProcessStatus processStatus = new ProcessStatus
                {
                    Name = processStatusName,
                };
                _db.ProcessStatus.Add(processStatus);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteProcessStatus(string processStatusName)
        {
            ProcessStatus processStatus = GetProcessStatusByName(processStatusName);

            if (processStatus != null)
            {
                _db.ProcessStatus.Remove(processStatus);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}