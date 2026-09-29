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

        public int CompanyID { get; set; }

        public decimal RatePerHour { get; set; }

        public string Currency { get; set; } = "R$";

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
        public DateTime? EffectiveTo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navegação
        public Company Company { get; set; } = null!;

        public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();


    }
}
