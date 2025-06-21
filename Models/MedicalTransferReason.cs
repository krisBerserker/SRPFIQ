using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class MedicalTransferReason
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Code { get; set; } //Référence interne
        [Required]
        [MaxLength(200)]
        [DisplayName("Nom")]
        public string Name { get; set; }
    }
}
