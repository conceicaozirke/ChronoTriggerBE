using ChronoTrigger.Models;



namespace ChronoTrigger.DTO
{
    public class TimeEntrySummary
    {
        public int ID { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string ActivityName {  get; set; }  = string.Empty;

        public string PersonName {  get; set; }  = string.Empty;

        public decimal HourlyRate { get; set; }

        public string Currency { get; set; } = "R$";

        public string DocumentTypeName {  get; set; } = string.Empty;

        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        public double HoursWorked { get; set; }

        public decimal TotalEarned { get; set; }

        public string? Notes { get; set; }


     


    
    }
}
