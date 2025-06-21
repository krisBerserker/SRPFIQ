using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class QuestionnaireDataSourceOptions
    {
        [Key]
        public int ID { get; set; }
        public int IdQuestionnaireDataSource { get; set; }
        public bool Active { get; set; } = true;
        [MaxLength(100)]
        public string DisplayText { get; set; }
        [ForeignKey(nameof(IdQuestionnaireDataSource))]
        public QuestionnaireDataSources? QuestionnaireDataSources { get; set; }
    }
}
