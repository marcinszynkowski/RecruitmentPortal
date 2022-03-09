namespace RP.Entities.AdminModule.ViewModels
{
    public class MoreInfoUserViewModel
    {
        public Account.User User { get; set; }
        public User.PersonalData PersonalData { get; set; }
        public User.EducationLevel EducationLevel { get; set; }
    }
}