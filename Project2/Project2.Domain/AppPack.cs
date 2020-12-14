using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppPack
    {
        public string PackId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime DateReleased { get; set; }

        public List<AppCard> PackCards { get; set; } = new List<AppCard>();
        //blank constructor
        public AppPack() { }
        public AppPack(string id, string name)
        {
            this.PackId = id;
            this.Name = name;
        }

    }
}
