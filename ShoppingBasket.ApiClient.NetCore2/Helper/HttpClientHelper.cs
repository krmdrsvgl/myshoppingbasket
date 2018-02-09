using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace ShoppingBasket.ApiClient.NetCore2.Helper
{
    public class HttpClientHelper
    {

        public  static  RestClient GetRestClient()
        {
            var client = new RestClient(ApiHelper.BaseUrl)
            {
                Authenticator = new HttpBasicAuthenticator(AppSettings.UserName, AppSettings.Password)
            };

            return client;
        }
    }
}
