using RP.Entities.User;
using System.Collections.Generic;

namespace RP.Entities.UserModule.ViewModels
{
    public class DrivingLivensesViewModel
    {
        public int UserId { get; set; }

        public int DrivingLicense_Id { get; set; } // lista rozwijana

        public List<DrivingLicense> list = new List<DrivingLicense>();

        public List<DrivingLicense> catList = new List<DrivingLicense>();

        public int selectedId { get; set;  }

        public int selectedCatId { get; set; }
    }
}
