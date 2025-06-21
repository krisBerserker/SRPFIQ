using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.Models
{
    public class Users
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("Prénoms")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200)]
        [DisplayName("Nom")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(250)]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Le nom d'utilisateur doit être un courriel.")]
        [DisplayName("Nom d'utilisateur")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Mot de passe")]
        public string PasswordHash { get; set; }
        [DisplayName("Date de dernière connexion")]
        public DateTime LastLoginDate { get; set; }
        public bool MustChangePassword { get; set; } = true; //Détermine si le mot de passe doit être changé à la prochaine connexion
        public bool Active { get; set; } = true; //Détermine si l'utilisateur est actif ou non
    }
}
