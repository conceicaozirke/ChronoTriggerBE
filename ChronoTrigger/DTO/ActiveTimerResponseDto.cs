using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChronoTrigger.Models;

namespace Models
{
    public class ActiveTimerResponseDto
    {
        public int ID { get; set; }

        public int TimerID { get; set; }

        public int ActivityID { get; set; }
        
        public int PersonID { get; set; }

        public string ActivityName { get; set; } = string.Empty;
        
        public string CompanyName { get; set; } = string.Empty;

        public TimeSpan StartTime { get; set; }
        
        public DateTime UpdatedAt { get; set; }

        public int AccumulatedSeconds { get; set; }

        public bool IsPaused { get; set; } = false;
    }
}
