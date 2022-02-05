using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            Sessions = new HashSet<Session>();
        }

        public int Id { get; set; }
        public string Vin { get; set; } = null!;
        public string DesignNumber { get; set; } = null!;
        public string? Iccid { get; set; }
        public string? Iccidc { get; set; }
        public string? Imei { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
