using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ActiveTimer
    {
        public int ID { get; set; }
        public int ActivityId { get; set; }
        public DateTime StartTime { get; set; }
        public int AccumulatedSeconds { get; set; } = 0;
        public bool IsPaused { get; set; } = false;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


        //navegação

        public Activity Activity { get; set; } =null!

    }
}
