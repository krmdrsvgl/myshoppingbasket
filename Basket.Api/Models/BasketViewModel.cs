using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace ShoppingBasket.Api.Models
{
    public class BasketViewModel
    {

        public List<BasketItemViewModel> Items = new List<BasketItemViewModel>();     
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public decimal PriceSum
        {
            get
            {
                return (decimal)Items.Sum(x => x.UnitPrice * x.Quantity);
            }
        }

        public int QuantitySum
        {
            get
            {
                return Items.Sum(x => x.Quantity);
            }
        }

        public int ItemCount
        {
            get
            {
                return Items.Count;
            }
        }
    }
}
