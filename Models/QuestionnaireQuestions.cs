using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class QuestionnaireQuestions
    {
        [Key]
        public int ID { get; set; }
        public int IdQuestionnaire { get; set; }
        public int Order { get; set; }
        [MaxLength(100)]
        [Required]
        [DisplayName("Nom")]
        public string Name { get; set; }
        public bool Active { get; set; } = true;
        [MaxLength(100)]
        [Required]
        [DisplayName("Titre")]
        public string Title { get; set; }
        [MaxLength(100)]
        [DisplayName("Sous-titre")]
        public string? ShortTitle { get; set; }
        [MaxLength(500)]
        [Required]
        [DisplayName("Instructions")]
        public string Instructions { get; set; }
        public int IdMainDataType { get; set; }
        public int? IdMainDataSource { get; set; }
        public int? IdSubDataType { get; set; }
        public int IdSubDataSource { get; set; }



        [ForeignKey(nameof(IdQuestionnaire))]
        public Questionnaires? Questionnaire { get; set; }
        [ForeignKey(nameof(IdMainDataSource))]
        
        public QuestionnaireDataSources? QuestionnaireDataSources { get; set; }

        public ICollection<QuestionnaireAnswerResults>? AnswerResults { get; set; }
    }
}
