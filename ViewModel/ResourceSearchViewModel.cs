using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.ViewModel
{
    public class RessourceSearchViewModel
    {
        public string SelectedBus { get; set; }
        public int? SelectedCategorieId { get; set; }
        public int? SelectedCityId { get; set; }

        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Quartiers { get; set; }
        public List<Resources> Resultats { get; set; }
    }

}
