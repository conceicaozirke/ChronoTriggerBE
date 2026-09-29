using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Activity
    {
        public int ID { get; set; }

        public int CompanyID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime LastUpdatedAt { get; set;} = DateTime.UtcNow;


        //navegação

        public Company Company { get; set; } = null!;
        public ICollection<TimeEntry> TimeEntries { get; set; } = new List<TimeEntry>();
        public ActiveTimer? ActiveTimer { get; set; }




    }
}
