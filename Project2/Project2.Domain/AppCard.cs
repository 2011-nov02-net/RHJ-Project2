using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppCard
    {
        public string Id { get; set; }
        public AppPack Pack { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Rarity { get; set; }
        public double Value { get; set; }

        AppCard(string id, AppPack pack)
        {
            this.Id = id;
            this.Pack = pack;
        }
        
    }
}
