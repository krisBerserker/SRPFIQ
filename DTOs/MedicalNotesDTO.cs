namespace WebApplication_SRPFIQ.DTOs
{
    public class MedicalNotesDTO
    {
        public int ID { get; set; }
        public int Intervention { get; set; }
        public string Date { get; set; }
        public string Heure { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
    }
}
