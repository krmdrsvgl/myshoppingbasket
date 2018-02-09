using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace ShoppingBasket.ApiClient.NetCore2.Helper
{
    public class HttpClientHelper
    {

        const string _baseUrl = "http://localhost:5001/api";

     
        public static  string GetUsersToken()
        {
            return "token";
        }

        public static T Execute<T>( RestRequest request) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = new System.Uri(_baseUrl),              
            };

            request.AddParameter("SecretId", AppSettings.SecretId, ParameterType.HttpHeader); // used on every request
            request.AddHeader("Authorization", string.Format("Bearer {0}", GetUsersToken()));// used on every request

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var apiException = new ApplicationException(message, response.ErrorException);
                throw apiException;
            }
            return response.Data;
        }
    }
}
