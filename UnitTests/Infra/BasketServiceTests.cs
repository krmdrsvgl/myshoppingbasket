using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using ShoppingBasket.Core.DomainServices;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Core.FilterObjects;
using ShoppingBasket.Core.Interfaces;
using ShoppingBasket.Core.Repositories;
using ShoppingBasket.Infrastructure.ApplicatonServices;
using ShoppingBasket.Infrastructure.Interfaces;
using Xunit;
using Shouldly;

namespace UnitTests.Infra
{
    public class BasketServiceTests
    {
        private string _testUserId1 = "testuserId1";
        private string _testItemId1 = "_testItemId1";
        private int _testQuantity1 = 2;
        private decimal _testprice1 = 2;

        readonly Mock<ILogger<InMemoryBasketRepository>> _mockBasketLogger =
            new Mock<ILogger<InMemoryBasketRepository>>();

        readonly Mock<ILogger<BasketService>> _mockServiceLogger = new Mock<ILogger<BasketService>>();
        readonly ICatalogService _catalogService = new InMemoryCatalogService();


        [Fact]
        public async Task ShouldCreateAndReturnaBasketForUserIfNotABasketPresent()
        {
            var inMemoryBasketRepo = new InMemoryBasketRepository(_mockBasketLogger.Object);
            var basketService = new BasketService(inMemoryBasketRepo, _catalogService, _mockServiceLogger.Object);

            await basketService.GetOrCreateBasketforUserAsync(_testUserId1);
            var basketinRepo =
                (await inMemoryBasketRepo.FindBasket(new BasketFilterObject {UserId = _testUserId1})).First();

            basketinRepo.UserId.ShouldBe(_testUserId1);
        }



        [Fact]
        public async Task ShouldCreateMultipleBaskets()
        {
            var inMemoryBasketRepo = new InMemoryBasketRepository(_mockBasketLogger.Object);
            var basketService = new BasketService(inMemoryBasketRepo, _catalogService, _mockServiceLogger.Object);

            await basketService.GetOrCreateBasketforUserAsync("user1");
            await basketService.GetOrCreateBasketforUserAsync("user2");
            await basketService.GetOrCreateBasketforUserAsync("user3");

            var basketCount =
                (await inMemoryBasketRepo.FindBasket(new BasketFilterObject { })).Count;

            basketCount.ShouldBe(3);
        }

        [Fact]
        public async Task ChangeQuantityoftheBasketItemAsync_ShouldThrowfQuantityisLessThanOne()
        {
            var inMemoryBasketRepo = new InMemoryBasketRepository(_mockBasketLogger.Object);
            var basketService = new BasketService(inMemoryBasketRepo, _catalogService, _mockServiceLogger.Object);

            await Assert.ThrowsAsync<ArgumentException>(() =>
                basketService.ChangeQuantityoftheBasketItemAsync(_testItemId1, 0, _testUserId1));

        }

        [Fact]
        public async Task ChangeQuantityoftheBasketShouldReturnNullIfNoItemIsPresentInBasket()
        {
            var inMemoryBasketRepo = new InMemoryBasketRepository(_mockBasketLogger.Object);
            var basketService = new BasketService(inMemoryBasketRepo, _catalogService, _mockServiceLogger.Object);
            var result =
                await basketService.ChangeQuantityoftheBasketItemAsync(_testItemId1, _testQuantity1, _testUserId1);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task ShouldNotAddBasketItem()
        {
            var inMemoryBasketRepo = new InMemoryBasketRepository(_mockBasketLogger.Object);
            var basketService =
                new BasketService(inMemoryBasketRepo, _catalogService, _mockServiceLogger.Object);

            await basketService.GetOrCreateBasketforUserAsync(_testUserId1);

            await basketService.AddItemToTheBasketAsync(null, 0, _testUserId1);

            var usersBasket = await basketService.GetOrCreateBasketforUserAsync(_testUserId1);

            usersBasket.Items.Count.ShouldBe(0);

        }

        [Fact]
        public async Task ChangeQuantityoftheBasketShouldSaveNewQuantityIfItemIsPresentInBasket()
        {
            var inMemoryBasketRepo = new InMemoryBasketRepository(_mockBasketLogger.Object);
            var basketService =
                new BasketService(inMemoryBasketRepo, _catalogService, _mockServiceLogger.Object);
            var basket = await basketService.GetOrCreateBasketforUserAsync(_testUserId1);

            var updatedBasket =
                await basketService.ChangeQuantityoftheBasketItemAsync(_testItemId1, _testQuantity1, _testUserId1);
            updatedBasket.ShouldBeNull();

            basket.AddItem(_testItemId1, 1, 1, null);

            updatedBasket =
                await basketService.ChangeQuantityoftheBasketItemAsync(_testItemId1, _testQuantity1, _testUserId1);
            updatedBasket.Items.First().Quantity.ShouldBe(_testQuantity1);
        }

        [Fact]
        public async Task RemovedItemShouldNotBePresentInBasketsItemsList()
        {
            var inMemoryBasketRepo = new InMemoryBasketRepository(_mockBasketLogger.Object);
            var basketService = new BasketService(inMemoryBasketRepo, _catalogService, _mockServiceLogger.Object);

            await basketService.AddItemToTheBasketAsync("item1", 1, _testUserId1);
            await basketService.AddItemToTheBasketAsync("item2", 1, _testUserId1);
            await basketService.AddItemToTheBasketAsync("item3", 1, _testUserId1);
            await basketService.AddItemToTheBasketAsync("item4", 1, _testUserId1);

            var basket = await basketService.GetOrCreateBasketforUserAsync(_testUserId1);
            basket.Items.Count.ShouldBe(4);


            basket = await basketService.RemoveItemFromtheBasketAsync("item1", _testUserId1);
            basket.Items.Count.ShouldBe(3);
            basket.Items.Any(x => x.ItemId == "item1").ShouldBe(false);

            basket = await basketService.RemoveItemFromtheBasketAsync("item2", _testUserId1);
            basket.Items.Count.ShouldBe(2);

            basket.Items.Any(x => x.ItemId == "item2").ShouldBe(false);

        }
    }
}
