using System;
using System.Collections.Generic;

namespace KSA_Collector.Tables
{
    public partial class ServiceCenter
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Postcode { get; set; } = null!;
        public string Region { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string DilerTr { get; set; } = null!;
    }
}
