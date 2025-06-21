using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication_SRPFIQ.Models
{
    public class UserAssignedRequests
    {
        [Key]
        public int ID { get; set; }
        public int IdRequest { get; set; }
        public int IdUser { get; set; }

        [ForeignKey(nameof(IdRequest))]
        public Requests? Requests { get; set; }

        [ForeignKey(nameof(IdUser))]
        public Users? Users { get; set; }
    }
}
