using RP.Entities.User;
using System.Collections.Generic;

namespace RP.Entities.UserModule.ViewModels
{
    public class EducationDataViewModel
    {
        
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

    //    public int EducationLevelId { get; set; }
        public string EducationLevelName { get; set; }

        public virtual EducationLevel EducationLevels { get; set; }

    //    public IEnumerable<EducationLevel> eduLevels = new List<EducationLevel>(); // zrobic metode zapelniajaca te liste (GetEducationLevels)
        public IEnumerable<Education> educationList;

        public int selectedId { get; set; } // id wybranego elementu z listy rozwijanej edycji

        public string NameOfSchool { get; set; }

        public string Major { get; set; }

        public string Minor { get; set; }
    }
}
