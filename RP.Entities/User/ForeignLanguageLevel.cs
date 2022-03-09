using System.ComponentModel.DataAnnotations;

namespace RP.Entities.User
{
    public class ForeignLanguageLevel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}