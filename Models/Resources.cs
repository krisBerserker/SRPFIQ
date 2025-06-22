using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class Resources
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Téléphone")]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Le Numéro de téléphone n'est pas valide.")]
        public string PhoneNumber { get; set; }
        [DisplayName("Quartier de l'organisation")]
        public int IdResourceCity { get; set; }
        [DisplayName("Adresse")]
        public string? Adresse { get; set; } //Adresse
        [DisplayName("Lignes d'autobus")]
        public string? BusNearBy { get; set; }

        [ForeignKey(nameof(IdResourceCity))]
        public ResourceCities? ResourceCity { get; set; }

        public ICollection<Resources_ResourceCatégories>? Resources_ResourceCategories { get; set; }
    }
}
