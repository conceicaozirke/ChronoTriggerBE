using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TimeEntry
    {
        public int ID {  get; set; }

        public int ActivityID { get; set; }

        public int HourlyRateId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //cronometro

        public TimeSpan Duration => EndTime - StartTime;
        public int DurationSeconds => (int)Duration.TotalSeconds;

        //navegação

        public Activity Activity { get; set; } = null!;
        public HourlyRate HourlyRate { get; set; } = null!;
                   
            


    }
}
