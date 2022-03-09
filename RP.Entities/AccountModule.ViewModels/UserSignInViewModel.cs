﻿using System.ComponentModel.DataAnnotations;

namespace RP.Entities.AccountModule.ViewModels
{
    public class UserSignInViewModel
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}