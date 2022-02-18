using System;
using System.Collections.Generic;

namespace TableModelLibrary.Models
{
    public partial class ServiceCenter
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Postcode { get; set; }
        public string? Region { get; set; }
        public string Username { get; set; } = null!;
        public string? Status { get; set; }
        public string? DilerTr { get; set; }
    }
}
