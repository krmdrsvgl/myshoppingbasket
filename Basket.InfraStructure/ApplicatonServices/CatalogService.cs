using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingBasket.Core.Entities;
using ShoppingBasket.Infrastructure.Interfaces;

namespace ShoppingBasket.Infrastructure.ApplicatonServices
{
    public class InMemoryCatalogService : ICatalogService
    {
        private readonly List<CatalogItem> _catalogItems = new List<CatalogItem>();

        public InMemoryCatalogService()
        {
            FillDummyCatalogItems();
        }

        public async Task<CatalogItem> GetCatalogItem(string itemId)
        {            
            return await Task.FromResult(_catalogItems.FirstOrDefault(x=>x.Id==itemId));
        }

        public async  Task<List<CatalogItem>> GetAll()
        {
            return await Task.FromResult(_catalogItems);
        }


        /// <summary>
        /// Creates dummy item for test purposes . Real world it should connect to another service to get catalog items.
        /// </summary>
        private void FillDummyCatalogItems()
        {
           var dummyItems= Enumerable.Range(1, 30).Select(i => new CatalogItem()
            {
                Id = $"item{i}",
                UnitPrice = i +(i/3),
                Name = $"Item{i}"
           });

            _catalogItems.AddRange(dummyItems);
        }
        

    }
}
