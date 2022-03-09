using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.User
{
    public class EducationLevel
    {
        [Key]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [Required]
        public string Name { get; set; }
    }
}