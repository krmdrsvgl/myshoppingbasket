using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Core.FilterObjects;
using ShoppingBasket.Core.Interfaces;

namespace ShoppingBasket.Core.Repositories
{
    public class InMemoryBasketRepository : IBasketRepository
    {
        private readonly ILogger<InMemoryBasketRepository> _logger;
        private List<Basket> _baskets = new List<Basket>();


        public InMemoryBasketRepository(ILogger<InMemoryBasketRepository> logger)
        {
            _logger = logger;            
        }

        public async Task<Basket> Update(Basket basket)
        {
            return await Task.FromResult(basket);
        }

        public async Task<Basket> AddBasket(Basket newBasket)
        {          
            _baskets.Add(newBasket);

            return await Task.FromResult(newBasket);
        }

        public async Task<bool> DeleteBasket(int basketId, string userId)
        {
            var basket = await GetBasket(basketId);

            if (basket == null)
            {
                _logger.LogInformation($"No basket found with id {basketId} ");
                return false;
            }

            _baskets.Remove(basket);

            return await Task.FromResult(true);
        }

        public async Task<Basket> GetBasket(int basketId)
        {
            return await Task.FromResult(_baskets.SingleOrDefault(x => x.Id == basketId));
        }
      
        public async Task<List<Basket>> FindBasket(BasketFilterObject searchInfo)
        {
            var basketQuery = _baskets.AsQueryable();           

            if (!string.IsNullOrEmpty(searchInfo.UserId))
            {
                basketQuery = basketQuery.Where(x => x.UserId == searchInfo.UserId);
            }

            if (searchInfo.BasketId>0)
            {
                basketQuery = basketQuery.Where(x => x.Id == searchInfo.BasketId);
            }

            return await Task.FromResult(basketQuery.ToList());
        }


        public async Task SaveChanges()
        {
            await Task.FromResult(true);
        }

    }
}