using System;

namespace ShoppingBasket.Core.Entities
{
    public class BasketItem
    {
        public string ItemId { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
