﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSA_Collector.TablesOld
{
    internal class System
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Domain { get; set; }
    }
}
