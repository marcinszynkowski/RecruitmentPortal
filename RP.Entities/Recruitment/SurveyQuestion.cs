using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.Recruitment
{
    public class SurveyQuestion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RecruitmentProcesses")]
        [Required]
        public int RecruitmentProcessId { get; set; }
        public virtual RecruitmentProcess RecruitmentProcesses { get; set; }

        [Required]
        [ForeignKey("SurveyQuestionTypes")]
        public int SurveyQuestionTypeId { get; set; }
        public virtual SurveyQuestionType SurveyQuestionTypes { get; set; }

        [Required]
        public string Question { get; set; }

        public int? ParentQuestionId { get; set; }
    }
}