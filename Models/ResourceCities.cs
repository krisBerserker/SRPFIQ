using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class ResourceCities
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("Nom du quartier")]
        public string Name { get; set; }
        public bool Active { get; set; } = true; //Détermine si le quartier est disponible pour sélection
        [DisplayName("Date de création système du quartier")]
        public DateTime CreatedDate { get; set; }
    }
}
