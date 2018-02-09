using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShoppingBasket.ApiClient.NetCore2.ApiServices.Basket.OutputDto
{
    class BasketItem
    {
        public List<BasketItem> Items = new List<BasketItem>();
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal PriceSum { get; set; }
        public int QuantitySum { get; set; }
        public int ItemCount { get; set; }
    }
}
