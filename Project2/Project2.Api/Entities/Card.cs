﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Project2.Api.Entities
{
    public partial class Card
    {
        public Card()
        {
            Auctions = new HashSet<Auction>();
            TradeDetailBuyerCards = new HashSet<TradeDetail>();
            TradeDetailOfferCards = new HashSet<TradeDetail>();
        }

        public string CardId { get; set; }
        public string PackId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Rarity { get; set; }
        public double Value { get; set; }

        public virtual Pack Pack { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<TradeDetail> TradeDetailBuyerCards { get; set; }
        public virtual ICollection<TradeDetail> TradeDetailOfferCards { get; set; }
    }
}