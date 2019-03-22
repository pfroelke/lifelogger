using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeLogger.Models.Entity
{
    public class LoggedItem
    {
        [Key]
        public long Id { get; set; }
        public Tag Tag { get; set; }
        public DateTime StartDate { get; set; }
        public long Duration { get; set; }
    }
}
