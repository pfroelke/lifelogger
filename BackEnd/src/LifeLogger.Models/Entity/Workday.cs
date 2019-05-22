using System;
using System.Collections.Generic;
using System.Text;

namespace LifeLogger.Models.Entity
{
    public class Workday
    {
        public string Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
    }
}
