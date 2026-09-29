using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public  class Company
    {
        public string ID { get; set; } = null!;

        public string? Name { get; set; }

        public int IDnumber { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public string? Description { get; set; }



        public ICollection<HourlyRate> HourlyRates { get; set; } = new List<HourlyRate>();
        public ICollection<Activity> Activitys { get; set; }= new List<Activity>();
    }
}
