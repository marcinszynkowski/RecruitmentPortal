using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class SendEmailUserViewModel
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Temat")]
        public string Subject { get; set; }

        [Display(Name = "Treść")]
        public string Body { get; set; }
    }
}