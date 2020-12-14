using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class StoreInventory
    {
        public int InvNum { get; set; }
        public string PackId { get; set; }
        public int PackQty { get; set; }

        public virtual Pack Pack { get; set; }
    }
}
