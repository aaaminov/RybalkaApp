using System;
using System.Collections.Generic;

namespace RybalkaApp.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Products = new HashSet<Product>();
        }

        public int UnitId { get; set; }
        public string UnitName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
