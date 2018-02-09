using System;
using System.Collections.Generic;
using System.Text;

namespace ShoppingBasket.Core.Entities
{
    public class ConsumerAuthInfo
    {
        public string UserId { get; set; }
        public string ConsumerSecretKey { get; set; }
    }
}
