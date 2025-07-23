using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApplication_SRPFIQ.Models;

namespace WebApplication_SRPFIQ.ViewModel
{
    public class ResourceDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string? Adresse { get; set; }

        public string Ville { get; set; }

        public List<string> Categories { get; set; } = new();

        public List<string> BusNearBy { get; set; } = new();

        public List<ResourceBusinessHours> businessHours { get; set; } = new();
        public List<Resources> Resultats { get; set; }


    }

}
