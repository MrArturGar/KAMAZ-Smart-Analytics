using System;
using System.Collections.Generic;

namespace TableModelLibrary.Table
{
    public partial class Dtc
    {

        public int Id { get; set; }
        public int IdEcu { get; set; }
        public string VehicleType { get; set; } = null!;
        public int Code { get; set; }
        public string TroubleCode { get; set; } = null!;

    }
}
