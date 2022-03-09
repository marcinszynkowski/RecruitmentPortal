using RP.Entities.Recruitment;
using RP.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.Account
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int AccessFailedCount { get; set; }

        public DateTime? LastLogin { get; set; }

        [Required]
        public bool LockoutEnabled { get; set; }

        public DateTime? PasswordLastModified { get; set; }

        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public virtual Role Roles { get; set; }

        public Guid? VirtualCVAdress { get; set; }

        public virtual ICollection<Skill> Skills { get; set; }
        public virtual ICollection<DrivingLicense> DrivingLicenses { get; set; }
        public virtual ICollection<WorkExperience> WorkExperience { get; set; }
        public virtual ICollection<Education> Education { get; set; }
        public virtual ICollection<UserForeignLanguage> UserForeignLanguages { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }
        public virtual ICollection<RecruitmentProcess> RecruitmentProcess { get; set; }
    }
}