using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class SendEmailUsersViewModel
    {
        [Display(Name = "Temat")]
        public string Subject { get; set; }

        [Display(Name = "Treść")]
        public string Body { get; set; }
    }
}