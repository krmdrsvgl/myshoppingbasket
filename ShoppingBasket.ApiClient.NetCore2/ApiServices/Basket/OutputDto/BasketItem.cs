using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasket.ApiClient.NetCore2.ApiServices.Basket.OutputDto
{
    public class BasketItem
    {
        public string String { get; set; }
        public decimal PriceSum { get; set; }
        public int QuantitySum { get; set; }
        public int ItemCount { get; set; }
    }
}
