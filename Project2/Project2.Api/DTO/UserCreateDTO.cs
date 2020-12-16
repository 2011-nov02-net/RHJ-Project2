using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project2.Api.DTO
{
    public class UserCreateDTO
    {
        // same as a view model, evolve over time
        
        public string UserId { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Email { get; set; }
        public int NumPacksPurchased { get; set; }
        [Range(0,10000)]
        public double CurrencyAmount { get; set; }
    }
}
