namespace WebApplication_SRPFIQ.ViewModel
{
    public class BusinessHourInput
    {
        public TimeOnly Opening { get; set; }
        public TimeOnly Closing { get; set; }

        // Un même bloc horaire peut s'appliquer à plusieurs jours
        public List<int> Days { get; set; } = new();
    }

}
