using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using ShoppingBasket.ApiClient.NetCore2.ApiServices.Basket;
using ShoppingBasket.ApiClient.NetCore2.Helper;

namespace ShoppingBasket.ApiClient.NetCore2
{
    public class ApiClient
    {       
        private BasketService _basketService;

        public BasketService ChargeService => _basketService ?? (_basketService = new BasketService());


        public ApiClient(string secretId)
        {
            AppSettings.SecretId = secretId;        
        }
       
    }
}
