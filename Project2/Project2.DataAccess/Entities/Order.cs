using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }

        public virtual Customer User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
