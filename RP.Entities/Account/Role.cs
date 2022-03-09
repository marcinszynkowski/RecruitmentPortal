using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.Account
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}