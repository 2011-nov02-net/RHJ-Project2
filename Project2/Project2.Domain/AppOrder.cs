using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppOrder
    {
        public string OrderId { get;}
        public AppUser Orderer { get; }
        //may require a list of PackIds to order multiple types of packs
        public AppPack Pack { get; }
        public int PackQty { get; }

        public DateTime Date { get; set; }
        //probably better as a method
        public double Total { get; set; }

        public AppOrder(string id, AppUser orderer, AppPack pack, int quantity)
        {
            this.OrderId = id;
            this.Orderer = orderer;
            this.Pack = pack;
            this.PackQty = quantity;
        }
    }
}
