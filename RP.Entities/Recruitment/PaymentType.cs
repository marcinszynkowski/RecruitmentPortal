using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.Recruitment
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<RecruitmentProcess> RecruitmentProcesses { get; set; }
    }
}