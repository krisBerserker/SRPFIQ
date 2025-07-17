using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.ViewModel
{
    public class ResourceCreateViewModel
    {
        public string Nom { get; set; }
        public int SelectedCityId { get; set; }

        public List<SelectListItem> Cities { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public List<int> SelectedCategoryIds { get; set; } = new();
        public List<string> BusList { get; set; } = new();

        public List<BusinessHourInput> BusinessHours { get; set; } = new();
    }


}
