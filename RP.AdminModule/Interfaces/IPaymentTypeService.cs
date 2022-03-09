using RP.Entities.Recruitment;
using System.Collections.Generic;

namespace RP.AdminModule.Interfaces
{
    public interface IPaymentTypeService
    {
        void CreatePaymentType(string paymentTypeName);
        void DeletePaymentType(int paymentTypeId);
        List<PaymentType> GetAllPaymentTypes();
        PaymentType GetPaymentTypeById(int? paymentTypeId);
        PaymentType GetPaymentTypeByName(string paymentTypeName);
        string GetPaymentTypeName(int paymentTypeId);
        void SetPaymentTypeName(int? paymentTypeId, string paymentTypeName);
    }
}