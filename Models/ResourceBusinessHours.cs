using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class ResourceBusinessHours
    {
        [Key]
        public int ID { get; set; }
        public int IdResource { get; set; }
        [DisplayName("Jour de la semaine")]
        public int DayOfWeek { get; set; }
        [DisplayName("Heure d'ouverture")]
        public TimeOnly OpeningTime { get; set; }
        [DisplayName("Heure de fermeture")]
        public TimeOnly ClosingTime { get; set; }

       
        [ForeignKey(nameof(IdResource))]
        public Resources? Resource { get; set; }
    }
}
