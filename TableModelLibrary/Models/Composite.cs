﻿using System;
using System.Collections.Generic;

namespace TableModelLibrary.Models
{
    public partial class Composite
    {
        public int Id { get; set; }
        public string DesignNumber { get; set; } = null!;
        public int IdEcu { get; set; }
    }
}