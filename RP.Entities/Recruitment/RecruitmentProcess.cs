using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
    
namespace RP.Entities.Recruitment
{
    public class RecruitmentProcess
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ProcessCode { get; set; }

        [Required]
        [ForeignKey("Companies")]
        public int CompanyId { get; set; }
        public virtual Company Companies { get; set; }

        [Required]
        [ForeignKey("Cities")]
        public int CityId { get; set; }
        public virtual City Cities { get; set; }

        [Required]
        [ForeignKey("Positions")]
        public int PositionId { get; set; }
        public virtual Position Positions { get; set; }

        [Required]
        public string Tasks { get; set; }

        [Required]
        public string Requirements { get; set; }

        [Required]
        public string Offer { get; set; }

        [ForeignKey("FormOfEmployments")]
        public int? FormOfEmploymentId { get; set; }
        public virtual FormOfEmployment FormOfEmployments { get; set; }

        [ForeignKey("PaymentTypes")]
        public int? PaymentTypeId { get; set; }
        public virtual PaymentType PaymentTypes { get; set; }

        public int? Payment { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [ForeignKey("ProcessStatus")]
        [Required]
        public int ProcessStatusId { get; set; }
        public virtual ProcessStatus ProcessStatus { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }

        public virtual ICollection<Account.User> Users { get; set; }
    }
}