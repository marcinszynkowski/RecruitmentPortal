using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.User
{
    [Table("PersonalData")]
    public class PersonalData
    {
        [Key]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime? Birthday { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public DateTime? LastModified { get; set; }
    }
}