using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppStoreItem
    {
        public string PackId { get; set; }
        public int PackQty { get; set; }

        public AppStoreItem() { }     

        public AppStoreItem(string id, int qty)
        {
            PackId = id;
            PackQty = qty;
        }
    }
}
