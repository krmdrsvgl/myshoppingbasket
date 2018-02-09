using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingBasket.Core.Entities;

namespace ShoppingBasket.Infrastructure.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetCatalogItem(string itemId);
        Task<List<CatalogItem>> GetAll();
    }
}