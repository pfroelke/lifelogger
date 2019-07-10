using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LifeLogger.Models.Entity
{
    public class Workday
    {
        // public virtual User Owner { get; set; }
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public TimeSpan WorkTime { get; set; }
        public float IncPerHour { get; set; }
        public float IncMade { get; set; }

        // [ForeignKey("UserId")]
        public virtual User Owner { get; set; }
    }
}
