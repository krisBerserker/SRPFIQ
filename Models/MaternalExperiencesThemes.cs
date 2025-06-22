using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class MaternalExperiencesThemes
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Code { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("Nom")]
        public string Name { get; set; }
        [DisplayName("Abordé en prénatal")]
        public bool IsPrenatal { get; set; }

        public ICollection<MaternalExperiences_MaternalExperienceThemes>? MaternalExperiences_MaternalExperiencesThemes { get; set; }
    }
}
