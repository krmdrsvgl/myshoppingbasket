using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Core.FilterObjects;

namespace ShoppingBasket.Core.Interfaces
{
    public interface IBasketRepository
    {       
        Task<bool> DeleteBasket(int basketId, string userId);
        Task<Basket> GetBasket(int basketId);
        Task<Basket> Update(Basket basket);
        Task<Basket> AddBasket(Basket basket);      
        Task<List<Basket>> FindBasket(BasketFilterObject searchInfo);
        Task SaveChanges();
    }
}