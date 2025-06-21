using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class Meetings
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Identifiant de la demande")]
        public int IdRequest { get; set; }
        [DisplayName("Utilisateur ayant inscrit la demande")]
        public int IdUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Numéro de la rencontre")]
        public int MeetingNumber { get; set; }
        [DisplayName("Date et heure de la rencontre")]
        public DateTime EventDate { get; set; }
        [Range(1, 3, ErrorMessage = "La valeur doit être comprise entre 1 et 3.")]
        [DisplayName("Type de rencontre")]
        public int IdMeetingType { get; set; }
        public int Amount { get; set; } //Représente la durée pour les types 1 et 3 et le nombre de textos por le type 2

        [DisplayName("Note")]
        public string? Note { get; set; }
        [DisplayName("Action")]
        public string? Action { get; set; }
        [DisplayName("Délai")]
        public string? Delay { get; set; }

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
