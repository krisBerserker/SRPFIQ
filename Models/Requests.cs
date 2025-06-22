using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class Requests
    {
        [Key]
        public int ID { get; set; } //Identifiant de la demande
        [Required]
        [MaxLength(25)]
        [DisplayName("Numéro de la demande")]
        public string FolioNumber { get; set; } //Numéro de la demande
        [Required, MaxLength(200)]
        [DisplayName("Nom complet de la mère")]
        public string FullName { get; set; } //Nom complet de la mère
        [MaxLength(200)]
        [DisplayName("Couverture santé")]
        public string? MedicalCoverage { get; set; } //Couverture santé
        [DisplayName("Demande reçue le")]
        public DateTime ReceivedRequestAt { get; set; } //Demande reçu le
        [MaxLength(50)]
        [DisplayName("Téléphone")]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Le Numéro de téléphone n'est pas valide.")]
        public string? PhoneNumber { get; set; } //Téléphone
        [MaxLength(250)]
        [DisplayName("Courriel")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "L'email n'est pas valide.")]
        public string? Email { get; set; } //Courriel
        [MaxLength(500)]
        [DisplayName("Adresse")]
        public string? Adresse { get; set; } //Adresse
        [MaxLength(20)]
        [DisplayName("Code Postal")]
        [RegularExpression(@"^[A-Za-z]\d[A-Za-z][ -]?\d[A-Za-z]\d$", ErrorMessage = "Le code postal est invalide.")]
        public string? ZipCode { get; set; } //Code Postal
        [DisplayName("Date prévue d'accouchement")]
        public DateTime? EstimatedDeliveryDate { get; set; } //Date prévue d'accouchement
        [MaxLength(50)]
        [DisplayName("Nombre d'accouchements")]
        public string NbPregnancy { get; set; } //Nombre d'accouchements
        [DisplayName("Est monoparental")]
        public bool IsMonoparental { get; set; } = false; //Est monoparental
        [MaxLength(50)]
        [DisplayName("Langue parlée")]
        public string? SpokenLanguage { get; set; } //Langue parlée
        [MaxLength(50)]
        [DisplayName("Status d'immigration")]
        public string? ImmigrationStatus { get; set; } //Status d'immigration
        [DisplayName("Nombre d'enfants")]
        public int? NbChilds { get; set; } //Nombre d'enfants
        [MaxLength(200)]
        [DisplayName("Age des enfants")]
        public string? ChildsAge { get; set; } //Age des enfants
        public int? IdUserClosedBy { get; set; } //Identifiant de l'utilisateur ayant fermé la demande
        [DisplayName("Date de fermeture de la demande")]
        public DateTime? ClosedDate { get; set; } //Date de fermeture de la demande
        [DisplayName("Date de création système du dossier")]
        public DateTime CreatedDate { get; set; } //Date de création système du dossier
        [DisplayName("Date de dernière modification")]
        public DateTime LastModifiedDate { get; set; } //Date de dernière modification système du dossier

        [ForeignKey(nameof(IdUserClosedBy))]
        public Users? UserClosedBy { get; set; }

        public ICollection<UserAssignedRequests>? AssignedUsers { get; set; }
        public ICollection<RequestNotes>? Notes { get; set; }
        public ICollection<MedicalNotes>? MedicalNotes { get; set; }
        public ICollection<Meetings>? Meetings { get; set; }
        public ICollection<QuestionnaireAnswers>? QuestionnaireAnswers { get; set; }
    }
}
