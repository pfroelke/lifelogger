using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeLogger.Core.Example
{
    public class ExampleEntity
    {
        public string ExampleName { get; set; }
        public string ExampleDescription { get; set; }

        [Key]
        public int ExampleId { get; set; }
    }
}
