using System.ComponentModel;

namespace WebApplication_SRPFIQ.DTOs
{
    public class RequestRelaiDTO
    {
        public int ID { get; set; }
        [DisplayName("Demande")]
        public string NumberFolio { get; set; }
        [DisplayName("Nom de la maman")]
        public string NameMother { get; set; }
        [DisplayName("DPA")]
        public DateTime? DPA { get; set; }
        [DisplayName("Date dernier suivi")]
        public DateTime? DateDernierSuivi {  get; set; }
        [DisplayName("Évaluation")]
        public string? Evaluation { get; set; }
        [DisplayName("Vécu maternel")]
        public string? VecuMaternel { get; set; }
        [DisplayName("Téléphone")]
        public string? PhoneNumber { get; set; }
        [DisplayName("Couverture santé")]
        public string? MedicalCoverage { get; set; }
        [DisplayName("Adresse")]
        public string? Adresse { get; set; }
        public bool IsMonoparental { get; set; }
        [DisplayName("Nb grossesse")]
        public string? NbPregnancy { get; set; }
        [DisplayName("Statut")]
        public string? Statut { get; set; }
        [DisplayName("Langue parlée")]
        public string? SpokenLanguage { get; set; }
        [DisplayName("Statut d'immigration")]
        public string? ImmigrationStatus { get; set; }
    }
}
