using System;
using System.Collections.Generic;

namespace RybalkaApp.Models
{
    public partial class Order
    {
        public Order()
        {
            Orderproducts = new HashSet<Orderproduct>();
        }

        public int OrderId { get; set; }
        public string OrderStatus { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public DateTime OrderDeliveryDate { get; set; }
        public int OrderPickupPointId { get; set; }

        public virtual Pickuppoint OrderPickupPoint { get; set; } = null!;
        public virtual ICollection<Orderproduct> Orderproducts { get; set; }
    }
}
