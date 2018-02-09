using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.ApiClient.NetCore2.ApiServices.Basket.OutputDto
{
    public class BasketInfo
    {
        public List<BasketItem> Items = new List<BasketItem>();
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal PriceSum { get; set; }
        public int QuantitySum { get; set; }
        public int ItemCount { get; set; }
    }
}
