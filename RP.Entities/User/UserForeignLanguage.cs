using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.User
{
    public class UserForeignLanguage
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [ForeignKey("ForeignLanguages")]
        [Required]
        public int ForeignLanguageId { get; set; }
        public virtual ForeignLanguage ForeignLanguages { get; set; }

        [ForeignKey("ForeignLanguageLevels")]
        [Required]
        public int ForeignLanguageLevelId { get; set; }
        public virtual ForeignLanguageLevel ForeignLanguageLevels { get; set; }
    }
}