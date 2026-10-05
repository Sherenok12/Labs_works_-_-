using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_2
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public LoyaltyCard? Card { get; set; }                 
        public List<Order> Orders { get; set; } = new();          
    }
}
