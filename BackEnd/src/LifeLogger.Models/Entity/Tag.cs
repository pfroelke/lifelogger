﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeLogger.Models.Entity
{
    public class Tag
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
