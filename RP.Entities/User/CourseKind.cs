using System.ComponentModel.DataAnnotations;

namespace RP.Entities.User
{
    public class CourseKind
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}