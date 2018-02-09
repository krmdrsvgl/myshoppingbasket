using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using ShoppingBasket.ApiClient.NetCore2.ApiServices.Basket.OutputDto;
using ShoppingBasket.ApiClient.NetCore2.Helper;

namespace ShoppingBasket.ApiClient.NetCore2.ApiServices.Basket
{
    public class BasketService
    {      
        public BasketInfo GetBasket()
        {
            var request = new RestRequest
            {
                Resource = "/basket"             ,
                Method = Method.GET
            };

            return HttpClientHelper.Execute<BasketInfo>(request);
            
        }

        public void ClearBasket()
        {
            var request = new RestRequest
            {
                Resource = "/clearAll",
                Method = Method.DELETE
            };

            HttpClientHelper.Execute<BasketInfo>(request);
        }

      

        public BasketInfo DeleteItem(string itemId)
        {
            var request = new RestRequest
            {
                Resource = $"/deleteItem/{itemId}",
                Method = Method.DELETE
            };

            return HttpClientHelper.Execute<BasketInfo>(request);
        }
      
        public BasketInfo UpdateQuantityOfTheItem(string id,  int quantity)
        {
            var request = new RestRequest
            {
                Resource = $"/changeQuantity/{id}",
                Method = Method.PUT
            };
            request.AddParameter("quantity", quantity, ParameterType.RequestBody);

            return HttpClientHelper.Execute<BasketInfo>(request);
        }

        public BasketInfo AddNewItem(string itemId, int quantity)
        {
            var request = new RestRequest
            {
                Resource = "/",
                Method = Method.POST
            };
            request.AddParameter("itemId", itemId, ParameterType.RequestBody);
            request.AddParameter("quantity", quantity, ParameterType.RequestBody);

            return HttpClientHelper.Execute<BasketInfo>(request);
        }

    }
}
