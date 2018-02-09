using System.Collections.Generic;
using ShoppingBasket.Api.Models;
using Shouldly;
using Xunit;

namespace UnitTests.Api.ViewModels
{

    public class BasketViewModelTests
    {


        [Fact]
        public void SumofQuantity_Price_And_Item_Count_Should_Correct()
        {
            var basketViewModel = new BasketViewModel
            {
                Items = {
                    new BasketItemViewModel
                        {
                            Quantity = 1,
                            UnitPrice = 1
                        },
                    new BasketItemViewModel
                    {
                        Quantity = 2,
                        UnitPrice = 2
                    }
               }
            };

            basketViewModel.PriceSum.ShouldBe(2*2+1);
            basketViewModel.ItemCount.ShouldBe(2);
            basketViewModel.QuantitySum.ShouldBe(2+1);
        }

    }
}
