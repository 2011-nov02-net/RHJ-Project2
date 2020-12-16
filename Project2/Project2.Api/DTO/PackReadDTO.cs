using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class PackReadDTO
    {
        public string PackId { get; set; }
        public string Name { get; set; }
        [Range(0,1000)]
        public double Price { get; set; }
        public DateTime DateReleased { get; set; }
    }
}
