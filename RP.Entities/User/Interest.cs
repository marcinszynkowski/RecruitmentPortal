using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.User
{
    public class Interest
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}