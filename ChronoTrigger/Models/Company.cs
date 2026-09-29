using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description {  get; set; }

        public string DocumentNumber { get; set; } = string.Empty;

        public string DocumentTypeID { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //navegação

        public ICollection<HourlyRate> HourlyRates { get; set; } = new List<HourlyRate>();
        public ICollection<Activity> Activities { get; set; }= new List<Activity>();



    }
}
