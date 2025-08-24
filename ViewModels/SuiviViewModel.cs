using System.ComponentModel;

namespace WebApplication_SRPFIQ.ViewModels
{
    public class SuiviViewModel
    {
        public int IDRequest { get; set; }
        public int Numero { get; set; }
        public DateTime Date { get; set; }
        [DisplayName("Type rencontre")]
        public string TypeRencontre { get; set; }
        [DisplayName("Durée")]
        public int Duree { get; set; }
        public string Notes { get; set; }
        public string Actions { get; set; }
        [DisplayName("Délais")]
        public string Delais { get; set; }
        public string Mode { get; set; } = "Ajout";
    }
}
