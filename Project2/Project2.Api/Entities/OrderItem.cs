using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Api.Entities
{
    public partial class OrderItem
    {
        public string OrderId { get; set; }
        public string PackId { get; set; }
        public int PackQty { get; set; }

        public virtual Order Order { get; set; }
        public virtual Pack Pack { get; set; }
    }
}
