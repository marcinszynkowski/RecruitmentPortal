using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AdminModule.ViewModels
{
    public class EditUserViewModel
    {
        [Display(Name = "Login")]
        public string Email { get; set; }

        [Display(Name = "Blokada konta")]
        public bool LockoutEnabled { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Rola")]
        public int RoleId { get; set; }
    }
}