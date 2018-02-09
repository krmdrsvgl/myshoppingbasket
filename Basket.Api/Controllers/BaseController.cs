using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShoppingBasket.Core.Interfaces;

namespace ShoppingBasket.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Base")]
    public class BaseController : Controller
    {        
        protected string GetCurrentUserId()
        {
            var bearer_token = "";                             
            return GetUserIdFromToken(bearer_token);
        }

        private string GetUserIdFromToken(string token)
        {
            return "user1";
        }
    }
}