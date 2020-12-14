using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppStoreItem
    {
        public string PackId { get; }
        public int Qty { get; set; }

        public AppStoreItem(string id, int qty)
        {
            PackId = id;
            Qty = qty;
        }
    }
}
