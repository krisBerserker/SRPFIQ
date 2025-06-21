using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class BirthPlaces
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        [DisplayName("Nom")]
        public string Name { get; set; }
        public bool Active { get; set; }//Si est disponible pour sélection
    }
}
