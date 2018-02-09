using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShoppingBasket.Api.Helpers;
using ShoppingBasket.Api.Models;
using ShoppingBasket.Core.Interfaces;

namespace ShoppingBasket.Api.Controllers
{
    
    [Route("api/basket")]
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        /// <summary>
        /// Here you can get current  basket of the customer. If customer doesn`t have a basket
        /// a new one will be created.
        /// </summary>
        /// <returns></returns>
        // GET: api/basket
        [HttpGet]
        public async Task<IActionResult> GetOrCreateBasket()
        {
            var usersBasket= await _basketService.GetOrCreateBasketforUserAsync(GetCurrentUserId());
            return Ok(ViewHelper.MapToBasketViewModel( usersBasket));
        }


        // POST: api/basket
        /// <summary>
        /// if item already exists in the basket, the only the quantity will be incremented. Otherwise item will be added to basket.
        /// </summary>
        /// <param name="item">Itemid and quantity fields are required. </param>
        /// <returns></returns>
        // POST: api/basket
        [HttpPost]
        public async Task<IActionResult> AddItemToBasket([FromBody] AddOrUpdateItemViewModel item )
        {          
            if (ModelState.IsValid)
            {
                var basket = await _basketService.AddItemToTheBasketAsync(item.ItemId, item.Quantity, GetCurrentUserId());
                if (basket != null)
                {
                    return Ok(ViewHelper.MapToBasketViewModel(basket));
                }

                return BadRequest();
            }
            
            return BadRequest(ModelState);
        }

        /// <summary>
        /// This will only change the quantity of the item in basket.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        /// 
        // PUT: api/basket/changeQuantity/{id}"
        [HttpPut("changeQuantity/{id}")]
        public async Task<IActionResult> UpdateQuantityOfTheItem(string id, [FromBody] int quantity)
        {
            if (ModelState.IsValid)
            {
                var basket = await _basketService.ChangeQuantityoftheBasketItemAsync(id, quantity, GetCurrentUserId());

                if (basket != null)
                {
                    return Ok(ViewHelper.MapToBasketViewModel(basket));
                }

                return BadRequest();
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Only deletes the item with the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// DELETE: api/basket/deleteItem/{id}"
        /// 
        [HttpDelete("deleteItem/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var basket = await _basketService.RemoveItemFromtheBasketAsync(id, GetCurrentUserId());

            if (basket != null)
            {
                return Ok(ViewHelper.MapToBasketViewModel(basket));
            }

            return BadRequest("Item could not be deleted. It does not exist.");
        }

        /// <summary>
        /// Clears all items in the basket.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("clearAll")]
        public async Task<IActionResult> ClearAll()
        {
            var result = await _basketService.ClearOutTheBasketAsync(GetCurrentUserId());

            if (result)
            {
                return Ok("Basket Cleared");
            }

            return BadRequest("Basket could not be Cleared");
        }
    }
}
