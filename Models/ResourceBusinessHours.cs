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
        public DaysOfWeek DayOfWeek { get; set; }
        [DisplayName("Heure d'ouverture")]
        public TimeOnly OpeningTime { get; set; }
        [DisplayName("Heure de fermeture")]
        public TimeOnly ClosingTime { get; set; }


        [ForeignKey(nameof(IdResource))]
        public Resources? Resource { get; set; }
    }

    public enum DaysOfWeek
    {
        Dimanche = 0,
        Lundi,
        Mardi,
        Mercredi,
        Jeudi,
        Vendredi,
        Samedi
    }
}
