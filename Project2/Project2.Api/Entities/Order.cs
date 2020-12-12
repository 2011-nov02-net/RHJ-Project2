using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Api.Entities
{
    public partial class Order
    {
        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public double Total { get; set; }

        public virtual Customer User { get; set; }
    }
}
