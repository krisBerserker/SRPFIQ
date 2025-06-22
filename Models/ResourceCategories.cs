using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class ResourceCategories
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("Nom de la catégorie")]
        public string Name { get; set; }
        public bool Active { get; set; } = true; //Détermine si la catégorie est disponible pour sélection
        [DisplayName("Date de création système de la catégorie")]
        public DateTime CreatedDate { get; set; }

        public ICollection<Resources_ResourceCategories>? Resources_ResourceCategories { get; set; }

    }
}
