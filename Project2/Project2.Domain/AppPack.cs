﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project2.Domain
{
    public class AppPack
    {
        public string Id { get; }
        public string Name { get; }
        public double Price { get; set; }
        public DateTime DateReleased { get; }

        public List<AppCard> PackCards { get; set; } = new List<AppCard>();
        AppPack(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
