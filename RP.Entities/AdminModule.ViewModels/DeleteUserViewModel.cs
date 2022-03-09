using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class DeleteUserViewModel
    {
        [Display(Name = "Login")]
        public string Email { get; set; }
    }
}