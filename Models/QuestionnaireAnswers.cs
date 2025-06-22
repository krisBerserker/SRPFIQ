using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class QuestionnaireAnswers
    {
        public int ID { get; set; }
        public int IdQuestionnaire { get; set; }
        public int IdRequest { get; set; }
        public int IdUser { get; set; }
        public int IdStatuts { get; set; }
        [DisplayName("Date de création système du dossier")]
        public DateTime CreatedDate { get; set; } //Date de création système du dossier
        [DisplayName("Date de dernière modification")]
        public DateTime LastModifiedDate { get; set; } //Date de dernière modification système du dossier



        [ForeignKey(nameof(IdQuestionnaire))]
        public Questionnaires? Questionnaires { get; set; }
        [ForeignKey(nameof(IdRequest))]
        public Requests? Requests { get; set; }
        [ForeignKey(nameof(IdUser))]
        public Users? Users { get; set; }

        public ICollection<QuestionnaireAnswerResults>? AnswerResults { get; set; }
    }
}
