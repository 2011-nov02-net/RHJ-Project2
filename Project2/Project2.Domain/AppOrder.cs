using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppOrder
    {
        public string Id { get;}
        public AppUser Orderer { get; set; }
        //may require a list of PackIds to order multiple types of packs
        public AppPack Pack { get; set; }
        public int PackQty { get; set; }

        public DateTime Date { get; set; }
        //probably better as a method
        public double Total { get; set; }

        AppOrder(string id, AppUser orderer, AppPack pack, int quantity)
        {
            this.Id = id;
            this.Orderer = orderer;
            this.Pack = pack;
            this.PackQty = quantity;
        }
    }
}
