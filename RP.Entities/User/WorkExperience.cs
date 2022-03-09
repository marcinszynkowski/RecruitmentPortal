using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.User
{
    [Table("WorkExperience")]
    public class WorkExperience
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [Required]
        public string Company { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public DateTime? DateFrom { get; set; }

        [Required]
        public DateTime? DateTo { get; set; }

        public string Responsibilities { get; set; }
    }
}