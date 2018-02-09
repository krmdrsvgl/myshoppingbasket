using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingBasket.Infrastructure.Interfaces;

namespace ShoppingBasket.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/CatalogItems")]
    public class CatalogItemsController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogItemsController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        /// <summary>
        /// You can seee all avaliable catalog items. Those are fake data to test.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _catalogService.GetAll();
            return Ok(items);
        }
    }
}