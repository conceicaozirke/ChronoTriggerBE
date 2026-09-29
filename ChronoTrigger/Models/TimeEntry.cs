using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TimeEntry
    {

        public int ID { get; set; }

        public int ActivityID { get; set; }

        public int HourlyRateID { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }


        //Duração em segundos do timer

        public TimeSpan Duration => EndTime - StartTime ;
        
        public int DurationInSeconds => (int) Duration.TotalSeconds;

        //navegação

        public  Activity Activity { get; set; } = null!;
        public HourlyRate HourlyRate { get; set; } = null!;


    }
}
