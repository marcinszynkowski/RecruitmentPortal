using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.User
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [ForeignKey("CourseTypes")]
        [Required]
        public int CourseTypeId { get; set; }
        public virtual CourseType CourseTypes { get; set; }

        [ForeignKey("CourseKinds")]
        [Required]
        public int CourseKindId { get; set; }
        public virtual CourseKind CourseKinds { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        [Required]
        public string Name { get; set; }
    }
}