using System;
using System.Collections.Generic;

namespace RybalkaApp.Models
{
    public partial class Productcategory
    {
        public Productcategory()
        {
            Products = new HashSet<Product>();
        }

        public int ProductCategoryId { get; set; }
        public string ProductCategoryName { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
