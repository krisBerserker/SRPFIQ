using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class MaternalExperiences
    {
        [Key]
        public int ID { get; set; }
        public int IdRequest { get; set; }
        [Required]
        [DisplayName("Date réel d'accouchement")]
        public DateTime BirthDate { get; set; }
        [DisplayName("Taille à la naissance(jours)")]
        public int? SizeAtBirthDays { get; set; }
        [DisplayName("Taille à la naissance(semaine)")]
        public int? SizeAtBithWeeks { get; set; }
        [DisplayName("Nom du bébé")]
        public string? BabyName { get; set; }
        [DisplayName("Sexe du bébé")]
        [MaxLength(1)]
        public string BabyGender { get; set; }
        public int? IdBirthPlace { get; set; }
        [DisplayName("Autre lieu d'accouchement")]
        [MaxLength(100)]
        public string? BirthPlaceOther { get; set; }
        [DisplayName("Accouchement naturel?")]
        public bool IsNaturalDelivery { get; set; } = false;
        [DisplayName("Déclenchement?")]
        public bool HadInductionLabor { get; set; }
        [DisplayName("Méthodes naturelles de soulagement?")]
        public bool HadNaturalReliefs { get; set; } = false;
        [DisplayName("Soutien psychologique?")]
        public bool HadPsychologicalSupport { get; set; } = false;
        [DisplayName("Rupture des membranes?")]
        public bool HadMembranesRupture { get; set; } = false;
        [DisplayName("Péridurale?")]
        public bool HadEpidural { get; set; } = false;
        [DisplayName("Autre anesthésiant?")]

        public bool HadOtherAnesthetic { get; set; } = false;
        [DisplayName("Épisiotomie?")]
        public bool HadEpisiotomy { get; set; } = false;
        [DisplayName("Ventouse et/ou forcep")]
        public bool HadSuctionCupsForceps { get; set; } = false;
        [DisplayName("Césarienne planifée?")]
        public bool HadPlannedCesarean { get; set; } = false;
        [DisplayName("Césarienne non planifée?")]
        public bool HadUnPlannedCesarean { get; set; } = false;
        [DisplayName("Décès?")]
        public bool HadDeceased { get; set; } = false;
        [DisplayName("Transfert?")]
        public bool HasBeenTranfered { get; set; } = false;
        [DisplayName("Identifiant de la raison du transfert")]
        public int IdMedicalTransferReason { get; set; }
        [DisplayName("Allaitement à la naissance?")]
        public bool IsBreastFeedingAtBirth { get; set; } = false;
        [DisplayName("Allaitement à six semaines?")]
        public bool IsBreastFeedingSixWeeks { get; set; } = false;

        [DisplayName("Raison si non")]
        public string? BreastFeedingNotes { get; set; }
        [DisplayName("Date de création système")]
        public DateTime CreatedDate { get; set; } //Date de création système du dossier
        [DisplayName("Date de dernière modification")]
        public DateTime LastModifiedDate { get; set; } //Date de dernière modification système du dossier


        [ForeignKey(nameof(IdRequest))]
        public Requests? Requests { get; set; }
        [ForeignKey(nameof(IdBirthPlace))]
        public BirthPlaces? BirthPlaces { get; set; }

        [ForeignKey(nameof(IdMedicalTransferReason))]
        public MedicalTransferReason? MedicalTransferReason { get; set; }
    }
}
