using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.User
{
    public class DrivingLicense
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Category { get; set; }

        public virtual ICollection<Account.User> Users { get; set; }
    }
}