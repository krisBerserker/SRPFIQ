namespace WebApplication_SRPFIQ.ViewModels
{
    public class MedicalNotesViewModel
    {
        public int ID { get; set; }
        public int IDRequest { get; set; }
        public string Intervention { get; set; }
        public string? Notes { get; set; }
        public DateTime Date { get; set; }
        public string? Heure { get; set; }
        public string? Description { get; set; }
        public string? UserName { get; set; }
        public string? Mode { get; set; }
    }
}
