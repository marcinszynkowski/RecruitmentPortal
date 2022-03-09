using RP.AdminModule.Interfaces;
using RP.Data.Context;
using RP.Entities.Recruitment;
using RP.RecruitmentModule.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RP.AdminModule.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        #region Fields
        private readonly RPDbContext _db;
        private readonly IRecruitmentProcessService _recruitmentProcessService;
        #endregion

        #region Constructors
        public PaymentTypeService(RPDbContext db)
        {
            _db = db;
        }

        public PaymentTypeService(RPDbContext db, IRecruitmentProcessService recruitmentProcessService)
        {
            _db = db;
            _recruitmentProcessService = recruitmentProcessService;
        }
        #endregion

        #region Get
        public PaymentType GetPaymentTypeByName(string paymentTypeName)
        {
            return _db.PaymentTypes.Where(pt => pt.Name == paymentTypeName).FirstOrDefault();
        }

        public PaymentType GetPaymentTypeById(int? paymentTypeId)
        {
            return _db.PaymentTypes.Find(paymentTypeId);
        }

        public string GetPaymentTypeName(int paymentTypeId)
        {
            return GetPaymentTypeById(paymentTypeId).Name;
        }

        public List<PaymentType> GetAllPaymentTypes()
        {
            return _db.PaymentTypes.ToList();
        }
        #endregion

        #region Set
        public void SetPaymentTypeName(int? paymentTypeId, string paymentTypeName)
        {
            PaymentType paymentType = GetPaymentTypeById(paymentTypeId);
            paymentType.Name = paymentTypeName;

            _db.SaveChanges();
        }
        #endregion

        #region Create
        public void CreatePaymentType(string paymentTypeName)
        {
            PaymentType paymentType = new PaymentType
            {
                Name = paymentTypeName,
            };

            if (GetPaymentTypeByName(paymentTypeName) == null)
            {
                _db.PaymentTypes.Add(paymentType);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeletePaymentType(int paymentTypeId)
        {
            PaymentType paymentType = GetPaymentTypeById(paymentTypeId);

            if (paymentType != null)
            {
                if (_recruitmentProcessService.GetRecruitmentProcessesByPaymentType(paymentType.Name) != null)
                {
                    List<RecruitmentProcess> list = _recruitmentProcessService.GetRecruitmentProcessesByPaymentType(paymentType.Name);
                    foreach (var l in list)
                    {
                        l.PaymentTypeId = null;
                    }
                }

                _db.PaymentTypes.Remove(paymentType);
                _db.SaveChanges();
            }
        }
    }
    #endregion
}