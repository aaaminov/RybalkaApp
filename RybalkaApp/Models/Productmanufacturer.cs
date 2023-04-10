using System;
using System.Collections.Generic;

namespace RybalkaApp.Models
{
    public partial class Productmanufacturer
    {
        public Productmanufacturer()
        {
            Products = new HashSet<Product>();
        }

        public int ProductManufacturerId { get; set; }
        public string ProductManufacturerName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
