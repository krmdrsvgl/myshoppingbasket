using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ShoppingBasket.Api.Controllers;
using ShoppingBasket.Api.Models;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Core.Interfaces;
using Xunit;
using  Shouldly;

namespace UnitTests.Api.Controllers
{
    public class BasketControllerTests
    {
        [Fact]
        public async Task Get_Should_Return_A_Basket()
        {
            var mockService = new Mock<IBasketService>();

            mockService.Setup(service => service.GetOrCreateBasketforUserAsync(It.IsAny<String>()))
                .Returns(Task.FromResult(GetBasket()));

            var mockLogger = new Mock<ILogger<BasketController>>();
            
            var controller = new BasketController(mockService.Object, mockLogger.Object);

            var result = await controller.Get();
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<BasketViewModel>(
                viewResult.Value);
                       
            model.ItemCount.ShouldBe(3);
            model.PriceSum.ShouldBe(2*2+1+1);
            model.PriceSum.ShouldBe(2*2+1+1);

        }

        private Basket GetBasket()
        {
            var basket = new Basket()
            {
                UserId = "Ekrem",
                Items = {
                    new BasketItem()
                    {
                        ItemId = "item1",
                        Quantity = 1,
                        UnitPrice = 1
                    },
                    new BasketItem()
                    {
                        ItemId = "item2",
                        Quantity = 2,
                        UnitPrice = 2
                    },
                    new BasketItem()
                    {
                        ItemId = "item3",
                        Quantity = 1,
                        UnitPrice = 1
                    },
                }
            };
            return basket;
        }
    }
}
