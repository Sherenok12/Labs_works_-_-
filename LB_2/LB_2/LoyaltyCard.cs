using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB_2
{
    public class LoyaltyCard
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CardNumber { get; set; } = "";
        public int DiscountPercent { get; set; }
    }
}
