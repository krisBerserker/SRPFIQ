using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class QuestionnaireDataSources
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(100)]
        [Required]
        [DisplayName("Nom")]
        public string Name { get; set; }
        public bool Active { get; set; } = false;
        [DisplayName("Date de création")]
        public DateTime CreatedDate { get; set; } //Date de création système

        public ICollection<QuestionnaireDataSourceOptions>? Options { get; set; }
    }
}
