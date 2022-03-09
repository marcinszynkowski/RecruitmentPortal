using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.Recruitment
{
    [Table("FormOfEmployment")]
    public class FormOfEmployment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<RecruitmentProcess> RecruitmentProcesses { get; set; }
    }
}