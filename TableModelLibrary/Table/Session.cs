using System;
using System.Collections.Generic;

namespace TableModelLibrary.Table
{
    public partial class Session
    {

        public int Id { get; set; }
        public int IdVehicle { get; set; }
        public string SessionsName { get; set; } = null!;
        public DateTime Date { get; set; }
        public int IdServiceCenters { get; set; }
        public string? VersionDb { get; set; }
        public string? Vcisn { get; set; }
        public double? Mileage { get; set; }
        public bool? HasIdentifications { get; set; }
        public bool? HasDtc { get; set; }
        public bool? HasTests { get; set; }
        public bool? HasFlash { get; set; }

    }
}
