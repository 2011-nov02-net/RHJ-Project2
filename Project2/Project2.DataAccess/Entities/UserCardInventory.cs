using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class UserCardInventory
    {
        public string UserId { get; set; }
        public string CardId { get; set; }
        public int Quantity { get; set; }

        public virtual Card Card { get; set; }
        public virtual Customer User { get; set; }
    }
}
