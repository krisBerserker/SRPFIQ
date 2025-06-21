using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class RequestNotes
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Identifiant de la demande")]
        public int IdRequest { get; set; }
        [DisplayName("Utilisateur ayant inscrit la demande")]
        public int IdUser { get; set; }
        [DisplayName("Note")]
        public string? Note { get; set; }
        [DisplayName("Date de création système du dossier")]
        public DateTime CreatedDate { get; set; } //Date de création système du dossier
        [DisplayName("Date de dernière modification")]
        public DateTime LastModifiedDate { get; set; } //Date de dernière modification système du dossier


        [ForeignKey(nameof(IdUser))]
        public Users? User { get; set; }
        [ForeignKey(nameof(IdRequest))]
        public Requests? Request { get; set; }
    }
}
