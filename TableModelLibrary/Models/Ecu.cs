﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TableModelLibrary.Models
{
    public partial class Ecu
    {
        public int Id { get; set; }
        public string Codifier { get; set; } = null!;
        public int IdControlSystem { get; set; }
    }
}
