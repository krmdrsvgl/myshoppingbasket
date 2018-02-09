using System;
using System.Collections.Generic;
using System.Text;
using ShoppingBasket.ApiClient.NetCore2.ApiServices.Basket;

namespace ShoppingBasket.ApiClient.NetCore2
{
    public class ApiClient
    {       
        private BasketService _basketService;

        public BasketService ChargeService => _basketService ?? (_basketService = new BasketService());


        public ApiClient(string userId, string password)
        {
            AppSettings.UserId = userId;
            AppSettings.Password = password;
        }

    }
}
