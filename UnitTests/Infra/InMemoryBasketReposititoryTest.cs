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
            await basketRepository.Add(newBasket);

            var basketInfo = (await basketRepository.Find(new BasketFilterObject())).First();

            basketInfo.UserId.ShouldBe(_testUserId);
            basketInfo.Id.ShouldBe(_testBasketId);
            
            //get by id an check values
            var basketGetById = (await basketRepository.Get(_testBasketId));
            basketGetById.UserId.ShouldBe(_testUserId);
            basketGetById.Id.ShouldBe(_testBasketId);

            var newBasket2 = new Basket()
            {
                UserId = _testUserId1,
                Id = _testBasketId1
            };

            //add a new Basket- There should be two baskets now.
            await basketRepository.Add(newBasket2);

            var basketFounSearchInfo = (await basketRepository.Find(new BasketFilterObject{BasketId = _testBasketId1})).First();
            basketFounSearchInfo.UserId.ShouldBe(_testUserId1);
            basketFounSearchInfo.Id.ShouldBe(_testBasketId1);

            var basketCount = (await basketRepository.Find(new BasketFilterObject())).Count();
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

            await basketRepository.Add(newBasket);
            await  Assert.ThrowsAsync<ArgumentNullException>(()=> basketRepository.Delete(0,""));           
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

            await basketRepository.Add(newBasket);
            var deleteResult = await basketRepository.Delete(_testBasketId, _testUserId);
            deleteResult.ShouldBe(true);
        }
    }
}
