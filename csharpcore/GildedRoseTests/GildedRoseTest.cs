using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Theory, MemberData(nameof(ItemTestData))]
        public void GivenItem_AfterUpdateQuality_ReturnsCorrectResult(IList<Item> items, IList<Item> expected)
        {
            var app = new GildedRose(items);
            app.UpdateQuality();
            Assert.Equal(expected[0].Name, items[0].Name);
            Assert.Equal(expected[0].Quality, items[0].Quality);
            Assert.Equal(expected[0].SellIn, items[0].SellIn);
        }

        public static IEnumerable<object[]> ItemTestData =>
            new List<object[]>
            {
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = -1, Quality = -1}},
                    new List<Item> {new Item {Name = "foo", SellIn = -2, Quality = -1}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = 0, Quality = 0}},
                    new List<Item> {new Item {Name = "foo", SellIn = -1, Quality = 0}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = 1, Quality = 1}},
                    new List<Item> {new Item {Name = "foo", SellIn = 0, Quality = 0}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = 3, Quality = 5}},
                    new List<Item> {new Item {Name = "foo", SellIn = 2, Quality = 4}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = -3, Quality = -5}},
                    new List<Item> {new Item {Name = "foo", SellIn = -4, Quality = -5}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 3, Quality = 5 } },
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 3, Quality = 5}}
                }
            };
    }
}
