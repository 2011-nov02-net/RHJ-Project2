using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppOrder
    {
        public string OrderId { get; set; }
        public string OrdererId { get; set; }
        public AppUser Orderer { get; set; }
        //may require a list of PackIds to order multiple types of packs
        public string PackId { get; set; }
        public AppPack Pack { get; set; }
        //public int PackQty { get; set; }

        public DateTime Date { get; set; }
        //probably better as a method
        public double Total { get; set; }

        public AppOrder(string id, AppUser orderer, AppPack pack)
        {
            this.OrderId = id;
            this.Orderer = orderer;
            this.Pack = pack;
            this.Total = Pack.PackQty * Pack.Price;
        }
        public AppOrder()
        {
        }

        /// <summary>
        /// executes an order of packs for the orderer
        /// </summary>
        /// <param name="orderer"></param>
        /// <param name="pack"></param>
        /// <returns>true on succesful order</returns>
        public bool FillOrder()
        {
            if (Orderer.CurrencyAmount >= Total)
            {
                Orderer.CurrencyAmount -= Total;
                int cards = 8 * Pack.PackQty;
                for (int i = 0; i < cards; ++i)
                {
                    //Orderer.AddCardToInventory(Pack.GetCard());
                }
                return true;
            }
            else
                return false;
        }
    }
}
