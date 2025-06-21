using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class MaternalExperiences_MaternalExperienceThemes
    {
        [Key]
        public int ID { get; set; }
        public int IdMaternalExperience { get; set; }
        public int IdMaternalExperienceTheme { get; set; }

        [ForeignKey(nameof(IdMaternalExperience))]
        public MaternalExperiences? MaternalExperiences { get; set; }
        [ForeignKey(nameof(IdMaternalExperienceTheme))]
        public MaternalExperiencesThemes? MaternalExperiencesThemes { get; set; }
    }
}
