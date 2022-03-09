using RP.Data.Context;
using RP.Entities.Recruitment;
using RP.Entities.RecruitmentModule.ViewModels;
using RP.RecruitmentModule.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace RP.RecruitmentModule.Services
{
    public class RecruitmentProcessService : IRecruitmentProcessService
    {
        #region Fields
        private readonly RPDbContext _db;
        #endregion

        #region Constructors
        public RecruitmentProcessService(RPDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Get
        public List<RecruitmentProcess> GetAllRecruitmentProcesses()
        {
            return _db.RecruitmentProcesses.ToList();
        }

        public RecruitmentProcess GetRecruitmentProcessByProcessCode(int processCode)
        {
            return _db.RecruitmentProcesses.Where(rp => rp.ProcessCode == processCode).FirstOrDefault();
        }

        public RecruitmentProcess GetRecruitmentProcessById(int processId)
        {
            return _db.RecruitmentProcesses.Find(processId);
        }

        public List<RecruitmentProcess> GetRecruitmentProcessesByCity(string city)
        {
            return GetAllRecruitmentProcesses().Where(rp => rp.Cities.Name.ToLower() == city.ToLower()).ToList();
        }

        public List<RecruitmentProcess> GetRecruitmentProcessesByPosition(string position)
        {
            return GetAllRecruitmentProcesses().Where(rp => rp.Positions.Name.ToLower() == position.ToLower()).ToList();
        }

        public List<RecruitmentProcess> GetRecruitmentProcessesByPaymentType(string paymentType)
        {
            return GetAllRecruitmentProcesses().Where(rp => rp.PaymentTypes.Name.ToLower() == paymentType.ToLower()).ToList();
        }

        public List<RecruitmentProcess> GetRecruitmentProcessesByFormOfEmployment(string formOfEmployment)
        {
            if (GetAllRecruitmentProcesses().Where(rp => rp.FormOfEmployments.Name == formOfEmployment) != null)
            {
                return GetAllRecruitmentProcesses().Where(rp => rp.FormOfEmployments.Name == formOfEmployment).ToList();
            }
            return null;
        }

        public List<RecruitmentProcess> GetRecruitmentProcessesByProcessStatus(int processStatusId)
        {
            return GetAllRecruitmentProcesses().Where(rp => rp.ProcessStatusId == processStatusId).ToList();
        }

        public List<RecruitmentProcess> GetRecruitmentProcessesByCompany(string company)
        {
            return GetAllRecruitmentProcesses().Where(rp => rp.Companies.Name.ToLower() == company.ToLower()).ToList();
        }

        public List<RecruitmentProcess> GetFilteredRecruitmentProcessesByAmmount(int ammountFrom, int ammountTo)
        {
            return GetAllRecruitmentProcesses().Where(rp => rp.Payment > ammountFrom && rp.Payment < ammountTo).ToList();
        }
        #endregion

        #region Set
        private void SetProcessCode(int processId, int processCode)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.ProcessCode = processCode;
        }

        private void SetCompany(int processId, int companyId)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.CompanyId = companyId;
        }

        private void SetCity(int processId, int cityId)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.CityId = cityId;
        }

        private void SetPosition(int processId, int positionId)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.PositionId = positionId;
        }

        private void SetTasks(int processId, string tasks)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.Tasks = tasks;
        }

        private void SetRequirements(int processId, string requirements)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.Requirements = requirements;
        }

        private void SetOffer(int processId, string offer)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.Offer = offer;
        }

        private void SetEmail(int processId, string email)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.Email = email;
        }

        private void SetPhone(int processId, string phone)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.Phone = phone;
        }

        private void SetFormOfEmployment(int processId, int? formOfEmploymentId)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.FormOfEmploymentId = formOfEmploymentId;
        }

        private void SetPaymentType(int processId, int? paymentTypeId)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.PaymentTypeId = paymentTypeId;
        }

        private void SetPayment(int processId, int? payment)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.Payment = payment;
        }

        private void SetProcessStatus(int processId, int processStatusId)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessById(processId);
            recruitmentProcess.ProcessStatusId = processStatusId;
        }
        #endregion

        #region Create
        public void CreateRecruitmentProcess(int processCode, int companyId, int cityId, int positionId, string tasks, string requirements, string offer, int? formOfEmploymentId, int? paymentTypeId, int? payment, string email, string phone)
        {
            RecruitmentProcess recruitmentProcess = new RecruitmentProcess
            {
                ProcessCode = processCode,
                CompanyId = companyId,
                CityId = cityId,
                PositionId = positionId,
                Tasks = tasks,
                Requirements = requirements,
                Offer = offer,
                FormOfEmploymentId = formOfEmploymentId,
                PaymentTypeId = paymentTypeId,
                Payment = payment,
                Email = email,
                Phone = phone,
                ProcessStatusId = 1,
            };

            _db.RecruitmentProcesses.Add(recruitmentProcess);
            _db.SaveChanges();
        }
        #endregion

        #region Delete
        public void DeleteRecruitmentProcess(int processCode)
        {
            RecruitmentProcess recruitmentProcess = GetRecruitmentProcessByProcessCode(processCode);

            if (recruitmentProcess != null)
            {
                _db.RecruitmentProcesses.Remove(recruitmentProcess);
                _db.SaveChanges();
            }
        }
        #endregion

        #region Edit
        public void EditRecruitmentProcess(int processId, RecruitmentProcess editedRecruitmentProcess)
        {
            try
            {
                SetCity(processId, editedRecruitmentProcess.CityId);
                SetCompany(processId, editedRecruitmentProcess.CompanyId);
                SetEmail(processId, editedRecruitmentProcess.Email);
                SetFormOfEmployment(processId, editedRecruitmentProcess.FormOfEmploymentId);
                SetOffer(processId, editedRecruitmentProcess.Offer);
                SetPayment(processId, editedRecruitmentProcess.Payment);
                SetPaymentType(processId, editedRecruitmentProcess.PaymentTypeId);
                SetPhone(processId, editedRecruitmentProcess.Phone);
                SetPosition(processId, editedRecruitmentProcess.PositionId);
                SetProcessCode(processId, editedRecruitmentProcess.ProcessCode);
                SetProcessStatus(processId, editedRecruitmentProcess.ProcessStatusId);
                SetRequirements(processId, editedRecruitmentProcess.Requirements);
                SetTasks(processId, editedRecruitmentProcess.Tasks);
                _db.SaveChanges();
            }
            catch
            {

            }
        }
        #endregion

        public IList<RecruitmentProcessViewModel> GetFilteredRecruitmentProcesses(FilterViewModel filterViewModel)
        {
            filterViewModel.RecruitmentProcessViewModelList = new List<RecruitmentProcessViewModel>();
            List<RecruitmentProcessViewModel> found = new List<RecruitmentProcessViewModel>();
            RecruitmentProcessViewModel recruitmentProcessViewModel;
            List<RecruitmentProcess> recruitmentProcesses = new List<RecruitmentProcess>();
            List<RecruitmentProcess> rpByCity = new List<RecruitmentProcess>();
            List<RecruitmentProcess> rpByCompany = new List<RecruitmentProcess>();
            List<RecruitmentProcess> rpByFormOfEmployment = new List<RecruitmentProcess>();
            List<RecruitmentProcess> rpByPosition = new List<RecruitmentProcess>();
            List<RecruitmentProcess> rpByPaymentType = new List<RecruitmentProcess>();
            List<FilterByCityViewModel> filterByCityViewModels = filterViewModel.CitiesViewModelList.Where(c => c.IsCityCheked == true).ToList();
            List<FilterByCompanyViewModel> filterByCompanyViewModels = filterViewModel.CompaniesViewModelList.Where(c => c.IsCompanyCheked == true).ToList();
            List<FilterByFormOfEmploymentViewModel> filterByFormOfEmploymentViewModels = filterViewModel.FormOfEmploymentViewModelList.Where(c => c.IsFormOfEmploymentCheked == true).ToList();
            List<FilterByPositionViewModel> filterByPositionViewModels = filterViewModel.PositionsViewModelList.Where(c => c.IsPositionCheked == true).ToList();
            List<FilterByPaymentTypeViewModel> filterByPaymentTypeViewModels = filterViewModel.PaymentTypesViewModelList.Where(c => c.IsPaymentTypeCheked == true).ToList();

            if ((filterByCityViewModels.Any() == false) && (filterByCompanyViewModels.Any() == false) && (filterByFormOfEmploymentViewModels.Any() == false) && (filterByPositionViewModels.Any() == false) && (filterByPaymentTypeViewModels.Any() == false))
            {
                recruitmentProcesses = GetAllRecruitmentProcesses();
                foreach (RecruitmentProcess rp in recruitmentProcesses)
                {
                    if (rp.ProcessStatusId == 1)
                    {
                        recruitmentProcessViewModel = new RecruitmentProcessViewModel
                        {
                            Id = rp.Id,
                            ProcessCode = rp.ProcessCode,
                            Company = rp.Companies.Name,
                            City = rp.Cities.Name,
                            Position = rp.Positions.Name,
                            Tasks = rp.Tasks,
                            Requirements = rp.Requirements,
                            Offer = rp.Offer,
                            FormOfEmployment = rp.FormOfEmployments.Name,
                            PaymentType = rp.PaymentTypes.Name,
                            Payment = rp.Payment,
                            Email = rp.Email,
                            Phone = rp.Phone,
                        };
                        filterViewModel.RecruitmentProcessViewModelList.Add(recruitmentProcessViewModel);
                    }
                }
                return filterViewModel.RecruitmentProcessViewModelList;
            }

            foreach (var city in filterByCityViewModels)
            {
                rpByCity.AddRange(GetRecruitmentProcessesByCity(city.City));
            }
            foreach (var company in filterByCompanyViewModels)
            {
                rpByCompany.AddRange(GetRecruitmentProcessesByCompany(company.Company));
            }
            foreach (var formOfEmployment in filterByFormOfEmploymentViewModels)
            {
                rpByFormOfEmployment.AddRange(GetRecruitmentProcessesByFormOfEmployment(formOfEmployment.FormOfEmployment));
            }
            foreach (var position in filterByPositionViewModels)
            {
                rpByPosition.AddRange(GetRecruitmentProcessesByPosition(position.Position));
            }
            foreach (var paymentType in filterByPaymentTypeViewModels)
            {
                rpByPaymentType.AddRange(GetRecruitmentProcessesByPaymentType(paymentType.PaymentType));
            }

            recruitmentProcesses = rpByCity.Intersect(rpByCompany).Intersect(rpByFormOfEmployment).Intersect(rpByPosition).ToList();

            if (filterByCompanyViewModels.Count == 0)
            {
                if (filterByPositionViewModels.Count == 0)
                {
                    if (filterByCityViewModels.Count == 0)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            recruitmentProcesses.AddRange(rpByPaymentType);
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                recruitmentProcesses.AddRange(rpByFormOfEmployment);
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPaymentTypeViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByFormOfEmployment.Where(rp => rp.PaymentTypes.Name == i.PaymentType));
                                }
                            }
                        }
                    }
                    else if (filterByCityViewModels.Count >= 1)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                recruitmentProcesses.AddRange(rpByCity);
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPaymentTypeViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByCity.Where(rp => rp.PaymentTypes.Name == i.PaymentType));
                                }
                            }
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByFormOfEmploymentViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByCity.Where(rp => rp.FormOfEmployments.Name == i.FormOfEmployment));
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByFormOfEmploymentViewModels)
                                {
                                    foreach (var j in filterByPaymentTypeViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByCity.Where(rp => rp.PaymentTypes.Name == j.PaymentType && rp.FormOfEmployments.Name == i.FormOfEmployment));
                                    }
                                }
                            }
                        }
                    }
                }
                else if (filterByPositionViewModels.Count >= 1)
                {
                    if (filterByCityViewModels.Count == 0)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                recruitmentProcesses.AddRange(rpByPosition);
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPaymentTypeViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByPosition.Where(rp => rp.PaymentTypes.Name == i.PaymentType));
                                }
                            }
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByFormOfEmploymentViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByPosition.Where(rp => rp.FormOfEmployments.Name == i.FormOfEmployment));
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByFormOfEmploymentViewModels)
                                {
                                    foreach (var j in filterByPaymentTypeViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByPosition.Where(rp => rp.PaymentTypes.Name == j.PaymentType && rp.FormOfEmployments.Name == i.FormOfEmployment));
                                    }
                                }
                            }
                        }
                    }
                    else if (filterByCityViewModels.Count >= 1)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByPosition.Where(rp => rp.Cities.Name == i.City));
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    foreach (var j in filterByPaymentTypeViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByPosition.Where(rp => rp.PaymentTypes.Name == j.PaymentType && rp.Cities.Name == i.City));
                                    }
                                }
                            }
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    foreach (var j in filterByFormOfEmploymentViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByPosition.Where(rp => rp.FormOfEmployments.Name == j.FormOfEmployment && rp.Cities.Name == i.City));
                                    }
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    foreach (var j in filterByFormOfEmploymentViewModels)
                                    {
                                        foreach (var k in filterByPaymentTypeViewModels)
                                        {
                                            recruitmentProcesses.AddRange(rpByPosition.Where(rp => rp.PaymentTypes.Name == k.PaymentType && rp.FormOfEmployments.Name == j.FormOfEmployment && rp.Cities.Name == i.City));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else if (filterByCompanyViewModels.Count >= 1)
            {
                if (filterByPositionViewModels.Count == 0)
                {
                    if (filterByCityViewModels.Count == 0)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                recruitmentProcesses.AddRange(rpByCompany);
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPaymentTypeViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.PaymentTypes.Name == i.PaymentType));
                                }
                            }
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByFormOfEmploymentViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.FormOfEmployments.Name == i.FormOfEmployment));
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByFormOfEmploymentViewModels)
                                {
                                    foreach (var j in filterByPaymentTypeViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.FormOfEmployments.Name == i.FormOfEmployment && rp.PaymentTypes.Name == j.PaymentType));
                                    }
                                }
                            }
                        }
                    }
                    else if (filterByCityViewModels.Count >= 1)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Cities.Name == i.City));
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    foreach (var j in filterByPaymentTypeViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Cities.Name == i.City && rp.PaymentTypes.Name == j.PaymentType));
                                    }
                                }
                            }
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    foreach (var j in filterByFormOfEmploymentViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Cities.Name == i.City && rp.FormOfEmployments.Name == j.FormOfEmployment));
                                    }
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByCityViewModels)
                                {
                                    foreach (var j in filterByFormOfEmploymentViewModels)
                                    {
                                        foreach (var k in filterByPaymentTypeViewModels)
                                        {
                                            recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Cities.Name == i.City && rp.PaymentTypes.Name == k.PaymentType && rp.FormOfEmployments.Name == j.FormOfEmployment));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (filterByPositionViewModels.Count >= 1)
                {
                    if (filterByCityViewModels.Count == 0)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position));
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    foreach (var j in filterByPaymentTypeViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position && rp.PaymentTypes.Name == j.PaymentType));
                                    }
                                }
                            }
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    foreach (var j in filterByFormOfEmploymentViewModels)
                                    {
                                            recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position && rp.FormOfEmployments.Name == j.FormOfEmployment));
                                    }
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    foreach (var j in filterByFormOfEmploymentViewModels)
                                    {
                                        foreach (var k in filterByPaymentTypeViewModels)
                                        {
                                            recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position && rp.PaymentTypes.Name == k.PaymentType && rp.FormOfEmployments.Name == j.FormOfEmployment));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (filterByCityViewModels.Count >= 1)
                    {
                        if (filterByFormOfEmploymentViewModels.Count == 0)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    foreach (var j in filterByCityViewModels)
                                    {
                                        recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position && rp.Cities.Name == j.City));
                                    }
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    foreach (var j in filterByCityViewModels)
                                    {
                                        foreach (var k in filterByPaymentTypeViewModels)
                                        {
                                            recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position && rp.PaymentTypes.Name == k.PaymentType && rp.Cities.Name == j.City));
                                        }
                                    }
                                }
                            }
                        }
                        else if (filterByFormOfEmploymentViewModels.Count >= 1)
                        {
                            if (filterByPaymentTypeViewModels.Count == 0)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    foreach (var j in filterByCityViewModels)
                                    {
                                        foreach (var k in filterByFormOfEmploymentViewModels)
                                        {
                                            recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position && rp.Cities.Name == j.City && rp.FormOfEmployments.Name == k.FormOfEmployment));
                                        }
                                    }
                                }
                            }
                            else if (filterByPaymentTypeViewModels.Count >= 1)
                            {
                                foreach (var i in filterByPositionViewModels)
                                {
                                    foreach (var j in filterByCityViewModels)
                                    {
                                        foreach (var k in filterByFormOfEmploymentViewModels)
                                        {
                                            foreach (var l in filterByPaymentTypeViewModels)
                                            {
                                                recruitmentProcesses.AddRange(rpByCompany.Where(rp => rp.Positions.Name == i.Position && rp.FormOfEmployments.Name == k.FormOfEmployment && rp.Cities.Name == j.City && rp.PaymentTypes.Name == l.PaymentType));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            recruitmentProcesses = recruitmentProcesses.Distinct().ToList();

            foreach (RecruitmentProcess rp in recruitmentProcesses)
            {
                if (rp.ProcessStatusId == 1)
                {
                    recruitmentProcessViewModel = new RecruitmentProcessViewModel
                    {
                        Id = rp.Id,
                        ProcessCode = rp.ProcessCode,
                        Company = rp.Companies.Name,
                        City = rp.Cities.Name,
                        Position = rp.Positions.Name,
                        Requirements = rp.Requirements,
                        Tasks = rp.Tasks,
                        Offer = rp.Offer,
                        FormOfEmployment = rp.FormOfEmployments.Name,
                        PaymentType = rp.PaymentTypes.Name,
                        Payment = rp.Payment,
                        Email = rp.Email,
                        Phone = rp.Phone,
                    };
                    filterViewModel.RecruitmentProcessViewModelList.Add(recruitmentProcessViewModel);
                }
            }
            return filterViewModel.RecruitmentProcessViewModelList;
        }
    }
}