using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingBasket.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Base")]
    public class BaseController : Controller
    {

        protected string GetCurrentUserId()
        {
            return "EkremDurmus";
        }
    }
}