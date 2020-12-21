using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class CardReadDTO
    {

        public string CardId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Rarity { get; set; }
        [Range(0, 1000)]
        public double Value { get; set; }
        [Range(0, 5)]
        public double Rating { get; set; }
        public int NumOfRatings { get; set; }
    }
}
