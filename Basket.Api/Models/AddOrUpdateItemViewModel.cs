using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingBasket.Api.Models
{
    public class AddOrUpdateItemViewModel
    {
        [Required]
        [MinLength(5)]
        public string ItemId { get; set; }

        [Required]
        [Range(1,1000)]
        public int Quantity { get; set; }
    }
}
