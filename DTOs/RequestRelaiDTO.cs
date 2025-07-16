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
    }
}
