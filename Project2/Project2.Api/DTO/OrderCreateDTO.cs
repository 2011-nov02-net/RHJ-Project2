using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class OrderCreateDTO
    {

        public string OrderId { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        [Range(0,10000)]
        public Double Total { get; set; }
        public string PackId { get; set; }
        public int PackQty { get; set; }
    }
}
