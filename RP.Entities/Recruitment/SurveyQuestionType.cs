using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RP.Entities.Recruitment
{
    public class SurveyQuestionType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<SurveyQuestion> SurveyQuestions { get; set; }
    }
}