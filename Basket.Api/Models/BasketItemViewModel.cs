using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Api.Models
{
    public class BasketItemViewModel
    {
        public string ItemId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
