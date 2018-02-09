using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Core.FilterObjects;
using ShoppingBasket.Core.Interfaces;
using ShoppingBasket.Infrastructure.Interfaces;

namespace ShoppingBasket.Core.DomainServices
{
    /// <summary>
    /// Basket service gets or creates Basket info based on userid. 
    /// If there is no basket present, it will create one. 
    /// 
    /// </summary>
    public class BasketService:IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly ICatalogService _catalogService;
        private readonly ILogger<BasketService> _logger;

        public BasketService(IBasketRepository basketRepository, ICatalogService catalogService, ILogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _catalogService = catalogService;
            _logger = logger;
        }

        public async Task<Basket> AddItemToTheBasketAsync(string itemId, int quantity, string userId)
        {
            var basket = await GetOrCreateBasketforUserAsync(userId);

            if (itemId == null || quantity < 1)
            {                 
                _logger.LogInformation($"ItemId or quantity cannot be null");

                return null;
            }

            var catalogItem = await _catalogService.GetCatalogItem(itemId);

            if (catalogItem == null)
            {
                _logger.LogInformation($"No catalog itemid found by id {itemId}");
                return null;
            }

            basket.AddItem(itemId, quantity, catalogItem?.UnitPrice, catalogItem?.Name);

            return await Task.FromResult(basket);
        }

        public async Task<Basket> RemoveItemFromtheBasketAsync(string itemId, string userId)
        {
            var basket = await GetOrCreateBasketforUserAsync(userId);

            var basketItem = basket.Items.SingleOrDefault(x => x.ItemId == itemId);

            if (basketItem != null)
            {
                basket.Items.Remove(basketItem);
                return await Task.FromResult(basket);
            }

            _logger.LogInformation($"No item found with id {itemId} ");

            return null;
        }

        public async Task<bool> ClearOutTheBasketAsync(string userId)
        {
            var basket = await GetOrCreateBasketforUserAsync(userId);

            basket.Items.Clear();

            await _basketRepository.Update(basket);

            return true;
        }

        public async Task<Basket> GetOrCreateBasketforUserAsync(string userId)
        {
            //find the first basket that belongs to user

            var basket = ( await _basketRepository.FindBasket(new BasketFilterObject() {UserId = userId}))?.FirstOrDefault();

            //if there is no basket present create one.
            if (basket == null)
            {
                basket= new Basket
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow
                };

                basket= await _basketRepository.AddBasket(basket);

                return basket;
            }

            return basket;
        }

        public async Task<Basket> ChangeQuantityoftheBasketItemAsync(string itemId, int newQuantity, string userId)
        {
            var basket = await GetOrCreateBasketforUserAsync(userId);

            if (newQuantity < 1)
            {
                throw new ArgumentException("Argument should be greater than 1");
            }
           
            var basketItem = basket.Items.SingleOrDefault(x => x.ItemId == itemId);

            if (basketItem != null)
            {
                basketItem.Quantity = newQuantity;
                return basket;
            }

            return null;
        }

        public async Task<int> GetBasketItemCount(string userId)
        {
            var basket = await GetOrCreateBasketforUserAsync(userId);

            return basket.Items.Sum(x => x.Quantity);
        }
    }
}
