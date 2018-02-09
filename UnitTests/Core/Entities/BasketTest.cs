using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShoppingBasket.Core.Entities;
using Xunit;
using Shouldly;

namespace UnitTests.Core.Entities
{

    public class BasketAddItem
    {
        private string _testItemdId = "item1";
        private decimal _testUnitPrice = 5.55m;
        private int _testQuantity = 3;
        private string _testName = "item1";

        [Fact]
        public void AddAnUnexistingItemToBasket()
        {
            var basket = new Basket();
            basket.AddItem(_testItemdId, _testQuantity, _testUnitPrice, null);

            var firstItem = basket.Items.Single();

            basket.Items.Count.ShouldBe(1);

            firstItem.ItemId.ShouldBe(_testItemdId);
            firstItem.UnitPrice.ShouldBe(_testUnitPrice);
            firstItem.Quantity.ShouldBe(_testQuantity);

        }


        
        [Fact]
        public void IfItemIsPresentQuantityShouldbeIncremented()
        {
            var basket = new Basket();
            basket.AddItem(_testItemdId, _testQuantity, _testUnitPrice, null);
            basket.AddItem(_testItemdId, _testQuantity, _testUnitPrice, null);

            var firstItem = basket.Items.Single();           
            firstItem.Quantity.ShouldBe(_testQuantity * 2);
        }


       

        [Fact]
        public void TestQuantityIfOnlyOneExists()
        {
            var basket = new Basket();
            basket.AddItem(_testItemdId,_testQuantity, _testUnitPrice, _testName);

            var firstItem = basket.Items.Single();          
            firstItem.Quantity.ShouldBe(_testQuantity);
            firstItem.Name.ShouldBe(_testName);
            firstItem.UnitPrice.ShouldBe(_testUnitPrice);
        }

        [Fact]
        public void AfterFiveDiffrerentItemThereShoulBeFiveItemsInTheBasket()
        {
            var basket = new Basket();
            for (int i = 1; i < 6; i++)
            {
                basket.AddItem(i.ToString(), i, i, null);
            }

            basket.Items.Count.ShouldBe(5);
        }

    }
}
