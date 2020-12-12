using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Api.Entities
{
    public partial class Pack
    {
        public Pack()
        {
            Cards = new HashSet<Card>();
        }

        public string PackId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime DateReleased { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}
