using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_SRPFIQ.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Nom")]
        public string LastName { get; set; }
        //[Required]
        //[Display(Name = "Nom d'utilisateur")]
        //public string Username { get; set; }

       

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le mot de passe")]
        [Compare("Password", ErrorMessage = "Le mot de passe et la confirmation ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Rôle")]
        public int SelectedRole { get; set; }

        public IEnumerable<SelectListItem>? Roles { get; set; }
    }
}
