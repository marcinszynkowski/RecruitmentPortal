using System;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.UserModule.ViewModels
{
    public class AddWorkExperienceViewModel
    {
        [Display(Name = "Stanowisko")]
        public string Position { get; set; }

        [Display(Name = "Data Od")]
        public DateTime DateFrom { get; set; }

        [Display(Name = "Data Do")]
        public DateTime DateTo { get; set; }

        [Display(Name = "Obowiązki")]
        public string Responsibilities { get; set; }

        [Display(Name = "Firma")]
        public string Company { get; set; }
    }
}
