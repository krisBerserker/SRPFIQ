using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class UserPermissions
    {
        [Key]
        public int ID { get; set; }
        public int IdUserRole { get; set; }
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdUserRole))]
        public UserRoles? UserRole { get; set; }
        [ForeignKey(nameof(IdUser))]
        public Users? Users { get; set; }
    }
}
