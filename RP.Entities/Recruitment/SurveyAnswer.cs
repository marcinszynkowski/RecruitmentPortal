using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RP.Entities.Recruitment
{
    public class SurveyAnswer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Users")]
        [Required]
        public int UserId { get; set; }
        public virtual Account.User Users { get; set; }

        [ForeignKey("SurveyQuestions")]
        [Required]
        public int SurveyQuestionId { get; set; }
        public virtual SurveyQuestion SurveyQuestions { get; set; }

        [Required]
        public string Answer { get; set; }
    }
}