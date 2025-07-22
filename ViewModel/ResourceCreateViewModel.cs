using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.ViewModel
{
    public class ResourceCreateViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "Veuillez entrer le nom de la ressource.")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire")]
        [RegularExpression(@"^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$", ErrorMessage = "Le numéro de téléphone n'est pas valide")]
        public string PhoneNumber { get; set; }

        [DisplayName("Adresse")]
        public string? Adresse { get; set; } //Adresse

        [Required(ErrorMessage = "Le champ Ville est requis.")]
        [Display(Name = "Ville")]
        public int SelectedCityId { get; set; }

        public List<SelectListItem> Cities { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();

        [Required(ErrorMessage = "Veuillez sélectionner au moins une catégorie.")]
        [Display(Name = "Catégories")]
        public List<int> SelectedCategoryIds { get; set; } = new();
        public List<string> BusList { get; set; } = new();

        public List<BusinessHourInput> BusinessHours { get; set; } = new();
    }


}
