using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class Resources_ResourceCategories
    {
        [Key]
        public int ID { get; set; }
        public int IdResourceCategory { get; set; }
        public int IdResource { get; set; }

        [ForeignKey(nameof(IdResourceCategory))]
        public ResourceCategories? ResourceCategory { get; set; }
        [ForeignKey(nameof(IdResource))]
        public Resources? Resource { get; set; }
    }
}
