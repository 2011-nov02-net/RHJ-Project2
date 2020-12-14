using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.DataAccess.Entities
{
    public partial class Pack
    {
        public string PackId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime DateReleased { get; set; }
    }
}
