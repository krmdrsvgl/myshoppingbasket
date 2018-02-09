using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Core.Entities
{
    public class CatalogItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
