using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class Questionnaires
    {
        public int ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public bool Active { get; set; } = false;
        [DisplayName("Date de création")]
        public DateTime CreatedDate { get; set; } //Date de création système

        public ICollection<QuestionnaireAnswers> Answers { get; set; }
        public ICollection<QuestionnaireQuestions> Questions { get; set; }
    }
}
