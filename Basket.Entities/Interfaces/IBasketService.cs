using System.Threading.Tasks;
using ShoppingBasket.Core.Entities;

namespace ShoppingBasket.Core.Interfaces
{
    public interface IBasketService
    {
        Task<Basket>  AddItemToTheBasketAsync(string itemId, int quantity, string buyerId);
        Task<Basket>  RemoveItemFromtheBasketAsync(string itemId, string userId);
        Task<bool>    ClearOutTheBasketAsync(string userId);
        Task<Basket>  GetOrCreateBasketforUserAsync(string userId);       
        Task<Basket>  ChangeQuantityoftheBasketItemAsync( string itemId, int newQuantity,string userId);
        Task<int>    GetBasketItemCount(string userName);
    }
}
