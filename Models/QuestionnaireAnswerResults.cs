using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class QuestionnaireAnswerResults
    {
        [Key]
        public int ID { get; set; }
        public int IdQuestionnaireAnswer { get; set; }
        public int IdQuestionnaireQuestion { get; set; }
        [MaxLength]
        public string? Value { get; set; }
        [ForeignKey(nameof(IdQuestionnaireAnswer))]
        public QuestionnaireAnswers? QuestionnaireAnswers { get; set; }
        [ForeignKey(nameof(IdQuestionnaireQuestion))]
        public QuestionnaireQuestions? QuestionnaireQuestions { get; set; }
    }
}
