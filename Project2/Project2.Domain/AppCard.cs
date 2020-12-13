using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppCard
    {
        public string CardId { get; set; }
        public AppPack Pack { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Rarity { get; set; }
        public double Value { get; set; }

        public AppCard()
        {
     
        }

        public AppCard(string id, AppPack pack)
        {
            CardId = id;
            Pack = pack;
        }
        
    }
}
