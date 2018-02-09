using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Core.FilterObjects;
using ShoppingBasket.Core.Repositories;
using Xunit;
using Shouldly;

namespace UnitTests.Infra
{
    public class InMemoryBasketReposititoryTest
    {

        string _testUserId = "user1";
        int _testBasketId = 1;


        string _testUserId1 = "user2";
        int _testBasketId1 = 2;       

        [Fact]
        public async Task NewlyAddedBasketShouldBeinRepository()
        {
            var basketRepository= new InMemoryBasketRepository(null);
         
            var newBasket = new Basket
            {
                UserId = _testUserId,  
                Id = _testBasketId
            };           

            //Add a new basket to in memory list and fetch and check
            await basketRepository.AddBasket(newBasket);

            var basketInfo = (await basketRepository.FindBasket(new BasketFilterObject())).First();

            basketInfo.UserId.ShouldBe(_testUserId);
            basketInfo.Id.ShouldBe(_testBasketId);
            
            //get by id an check values
            var basketGetById = (await basketRepository.GetBasket(_testBasketId));
            basketGetById.UserId.ShouldBe(_testUserId);
            basketGetById.Id.ShouldBe(_testBasketId);

            var newBasket2 = new Basket()
            {
                UserId = _testUserId1,
                Id = _testBasketId1
            };

            //add a new Basket- There should be two baskets now.
            await basketRepository.AddBasket(newBasket2);

            var basketFounSearchInfo = (await basketRepository.FindBasket(new BasketFilterObject{BasketId = _testBasketId1})).First();
            basketFounSearchInfo.UserId.ShouldBe(_testUserId1);
            basketFounSearchInfo.Id.ShouldBe(_testBasketId1);

            var basketCount = (await basketRepository.FindBasket(new BasketFilterObject())).Count();
            basketCount.ShouldBe(2);

        }


        [Fact]
        public async Task WithFalseIdNothingShouldBeDeleted()
        {
            var basketRepository = new InMemoryBasketRepository(null);

            var newBasket = new Basket()
            {
                UserId = _testUserId,
                Id = _testBasketId
            };

            await basketRepository.AddBasket(newBasket);
            await  Assert.ThrowsAsync<ArgumentNullException>(()=> basketRepository.DeleteBasket(0,""));           
        }

        [Fact]
        public async Task WithTrueValuesBasketShouldBeDeleted()
        {
            var basketRepository = new InMemoryBasketRepository(null);

            var newBasket = new Basket()
            {
                UserId = _testUserId,
                Id = _testBasketId
            };

            await basketRepository.AddBasket(newBasket);
            var deleteResult = await basketRepository.DeleteBasket(_testBasketId, _testUserId);
            deleteResult.ShouldBe(true);
        }
    }
}
