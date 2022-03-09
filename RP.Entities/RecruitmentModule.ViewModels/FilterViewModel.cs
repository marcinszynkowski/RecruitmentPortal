using System.Collections.Generic;

namespace RP.Entities.RecruitmentModule.ViewModels
{
    public class FilterViewModel
    {
        public IList<FilterByCompanyViewModel> CompaniesViewModelList { get; set; }

        public IList<FilterByCityViewModel> CitiesViewModelList { get; set; }

        public IList<FilterByPositionViewModel> PositionsViewModelList { get; set; }

        public IList<FilterByFormOfEmploymentViewModel> FormOfEmploymentViewModelList { get; set; }

        public IList<FilterByPaymentTypeViewModel> PaymentTypesViewModelList { get; set; }

        public IList<RecruitmentProcessViewModel> RecruitmentProcessViewModelList { get; set; }
    }
}