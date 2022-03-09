using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.User
{
    [Table("Education")]
    public class Education
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [Required]
        public string NameOfSchool { get; set; }

        public string Major { get; set; }

        public string Minor { get; set; }
    }
}