using System.ComponentModel.DataAnnotations;

namespace RP.Entities.UserModule.ViewModels
{
    public class DeleteUserAccountViewModel
    {
        [Display(Name = "Login")]
        public string Email { get; set; }
    }
}
