using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class UserRoles
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(200)]
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}
