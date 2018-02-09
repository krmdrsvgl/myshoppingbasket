using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingBasket.Core.Entities
{

    /// <summary>
    /// Main entity that holds the info shopping basket info.
    /// </summary>
    public  class Basket:BaseEntity
    {

        public readonly List<BasketItem> Items = new List<BasketItem>();
       

        /// <summary>
        /// Ensure there is only distinct items on the basket. Insertion of existing item insert will increase the quantity. 
        /// </summary>
        /// <param name="itemId"> id of the inserted  catalog item</param>
        /// <param name="unitPrice"> </param>
        /// <param name="quantity"></param>
        /// 
        public void AddItem(string itemId, int quantity, decimal? unitPrice, string name)
        {
            if (Items.All(item => item.ItemId != itemId))
            {
                Items.Add(new BasketItem
                {
                    ItemId = itemId,
                    Quantity = quantity,                   
                    UnitPrice = unitPrice,
                    Name = name,
                    AddedAt = DateTime.UtcNow
                });

                return;
            }

            var currentItem = Items.FirstOrDefault(i => i.ItemId == itemId);

            if (currentItem != null) currentItem.Quantity += quantity;
        }


        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }        
        
    }

   
}