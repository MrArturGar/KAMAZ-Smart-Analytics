using System;
using System.Collections.Generic;

namespace KSA_API.Views
{
    public partial class ServiceCenter
    {
        public ServiceCenter()
        {
            Sessions = new HashSet<Session>();
        }

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

        public virtual ICollection<Session> Sessions { get; set; }
    }
}
