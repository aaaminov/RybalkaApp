using System;
using System.Collections.Generic;

namespace RybalkaApp.Models
{
    public partial class Pickuppoint
    {
        public Pickuppoint()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int IndexAddress { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? House { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
