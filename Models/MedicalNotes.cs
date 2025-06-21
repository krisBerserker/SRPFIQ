using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class MedicalNotes
    {
        [Key]
        public int ID { get; set; }
        public int IdRequest { get; set; } //Identifiant de la demande
        public int IdUser { get; set; } //Identifiant de l'utilisateur
        [DisplayName("Date et heure de l'évènement")]
        public DateTime EventDate { get; set; }
        [Required]
        [MaxLength]
        [DisplayName("Description de l'intervention médical")]
        public string Description { get; set; }
        [MaxLength]
        [DisplayName("Notes")]
        public string? Notes { get; set; }
        [DisplayName("Date de création système du dossier")]
        public DateTime CreatedDate { get; set; } //Date de création système du dossier
        [DisplayName("Date de dernière modification")]
        public DateTime LastModifiedDate { get; set; } //Date de dernière modification système du dossier


        [ForeignKey(nameof(IdRequest))]
        public Requests? Requests { get; set; }
        [ForeignKey(nameof(IdUser))]
        public Users? Users { get; set; }
    }
}
