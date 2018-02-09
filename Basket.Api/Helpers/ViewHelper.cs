using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingBasket.Api.Models;
using ShoppingBasket.Core.Entities;

namespace ShoppingBasket.Api.Helpers
{
    public class ViewHelper
    {

        public static  BasketViewModel MapToBasketViewModel(Basket basket)
        {
            var basketView = new BasketViewModel
            {
                CreatedAt = basket.CreatedAt,
                UserId = basket.UserId,
                Items = basket.Items.Select(item => new BasketItemViewModel()
                {
                    Name = item.Name,
                    UnitPrice = item.UnitPrice,
                    AddedAt = item.AddedAt,
                    Quantity = item.Quantity,
                    ItemId = item.ItemId
                }).ToList()
            };

            return  basketView;
        }
    }
}
