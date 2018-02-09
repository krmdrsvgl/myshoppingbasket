using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Core.FilterObjects;

namespace ShoppingBasket.Core.Interfaces
{
    public interface IBasketRepository
    {       
        Task<bool> Delete(int basketId, string userId);
        Task<Basket> Get(int basketId);
        Task<Basket> Update(Basket basket);
        Task<Basket> Add(Basket basket);      
        Task<List<Basket>> Find(BasketFilterObject searchInfo);
        Task SaveChanges();
    }
}