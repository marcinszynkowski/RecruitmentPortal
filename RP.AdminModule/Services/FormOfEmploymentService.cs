using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Recruitment;
using RP.RecruitmentModule.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class FormOfEmploymentService : IFormOfEmploymentService
    {
        #region Fields
        private readonly RPDbContext _db;
        private readonly IRecruitmentProcessService _recruitmentProcessService;
        #endregion

        #region Constructors
        public FormOfEmploymentService(RPDbContext db)
        {
            _db = db;
        }

        public FormOfEmploymentService(RPDbContext db, IRecruitmentProcessService recruitmentProcessService)
        {
            _db = db;
            _recruitmentProcessService = recruitmentProcessService;
        }
        #endregion

        #region Get
        public FormOfEmployment GetFormOfEmploymentByName(string formOfEmploymentName)
        {
            return _db.FormOfEmployment.Where(foe => foe.Name == formOfEmploymentName).FirstOrDefault();
        }

        public FormOfEmployment GetFormOfEmploymentById(int? formOfEmploymentId)
        {
            if (formOfEmploymentId == null)
            {
                return null;
            }
            return _db.FormOfEmployment.Find(formOfEmploymentId);
        }

        public string GetFormOfEmploymentName(int formOfEmploymentId)
        {
            return GetFormOfEmploymentById(formOfEmploymentId).Name;
        }

        public List<FormOfEmployment> GetAllFormOfEmployment()
        {
            return _db.FormOfEmployment.ToList();
        }
        #endregion

        #region Set
        public void SetFormOfEmploymentName(int? formOfEmploymentId, string formOfEmploymentName)
        {
            FormOfEmployment formOfEmployment = GetFormOfEmploymentById(formOfEmploymentId);
            formOfEmployment.Name = formOfEmploymentName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreateFormOfEmployment(string formOfEmploymentName)
        {
            if (GetFormOfEmploymentByName(formOfEmploymentName) == null)
            {
                FormOfEmployment formOfEmployment = new FormOfEmployment
                {
                    Name = formOfEmploymentName,
                };
                _db.FormOfEmployment.Add(formOfEmployment);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteFormOfEmployment(int formOfEmploymentId)
        {
            FormOfEmployment formOfEmployment = GetFormOfEmploymentById(formOfEmploymentId);

            if (formOfEmployment != null)
            {
                if (_recruitmentProcessService.GetRecruitmentProcessesByFormOfEmployment(formOfEmployment.Name) != null)
                {
                    List<RecruitmentProcess> list = _recruitmentProcessService.GetRecruitmentProcessesByFormOfEmployment(formOfEmployment.Name);
                    foreach (var l in list)
                    {
                        l.FormOfEmploymentId = null;
                    }
                }

                _db.FormOfEmployment.Remove(formOfEmployment);
                _db.SaveChanges();
            }
        }
        #endregion
    }
}