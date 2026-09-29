using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class HourlyRate
    {
        public int ID { get; set; }

        public string? CompanyID { get; set; }
        
        public decimal RatePerHour { get; set; }

        public string Currency { get; set; } = "BRL";

        public DateTime EffectiveFrom { get; set; }

        public DateTime Effectiveto { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }


        //navegação
        public Company company { get; set; } = null!;

        public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();





    }
}
