using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class StoreDTO
    {
        public string PackId { get; set; }
        [Range(0,10000)]
        public int PackQty { get; set; }


    }
}
