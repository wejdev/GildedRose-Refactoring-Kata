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
            for (var i = 0; i < items.Count; i++)
            {
                Assert.Equal(expected[i].Name, items[i].Name);
                Assert.Equal(expected[i].Quality, items[i].Quality);
                Assert.Equal(expected[i].SellIn, items[i].SellIn);
            }
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
                // Once the sell by date has passed, Quality degrades twice as fast
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = -1, Quality = 24}},
                    new List<Item> {new Item {Name = "foo", SellIn = -2, Quality = 22}}
                },
                // The Quality of an item is never negative
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = 5, Quality = 0}},
                    new List<Item> {new Item {Name = "foo", SellIn = 4, Quality = 0}}
                },
                new object[]
                {
                    new List<Item> {
                        new Item {Name = "foo", SellIn = 51, Quality = 51},
                        new Item {Name = "foo", SellIn = 50, Quality = 50},
                        new Item {Name = "foo", SellIn = 49, Quality = 49},
                        new Item {Name = "foo", SellIn = 12, Quality = 12},
                        new Item {Name = "foo", SellIn = 11, Quality = 11},
                        new Item {Name = "foo", SellIn = 10, Quality = 10},
                        new Item {Name = "foo", SellIn = 7, Quality = 7},
                        new Item {Name = "foo", SellIn = 6, Quality = 6},
                        new Item {Name = "foo", SellIn = 5, Quality = 5},
                        new Item {Name = "foo", SellIn = 100, Quality = 100},
                        new Item {Name = "foo", SellIn =-40, Quality = -50}},
                    new List<Item> {
                        new Item {Name = "foo", SellIn = 50, Quality = 50},
                        new Item {Name = "foo", SellIn = 49, Quality = 49},
                        new Item {Name = "foo", SellIn = 48, Quality = 48},
                        new Item {Name = "foo", SellIn = 11, Quality = 11},
                        new Item {Name = "foo", SellIn = 10, Quality = 10},
                        new Item {Name = "foo", SellIn = 9, Quality = 9},
                        new Item {Name = "foo", SellIn = 6, Quality = 6},
                        new Item {Name = "foo", SellIn = 5, Quality = 5},
                        new Item {Name = "foo", SellIn = 4, Quality = 4},
                        new Item {Name = "foo", SellIn = 99, Quality = 99},
                        new Item {Name = "foo", SellIn = -41, Quality = -50}}
                },
                // “Aged Brie” actually increases in Quality the older it gets
                new object[]
                {
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -1, Quality = 5 }},
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -2, Quality = 7 }}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -22, Quality = 45 }},
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -23, Quality = 47 }}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -6, Quality = 48 }},
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -7, Quality = 50 }}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -6, Quality = 49 }},
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -7, Quality = 50 }}
                },
                // The Quality of an item is never more than 50
                new object[]
                {
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -6, Quality = 50 }},
                    new List<Item> {new Item {Name = "Aged Brie", SellIn = -7, Quality = 50 }}
                },
                // "Sulfuras", being a legendary item, never has to be sold or decreases in Quality
                new object[]
                {
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 3, Quality = 5 }},
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 3, Quality = 5 }}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -2, Quality = 0 }},
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -2, Quality = 0 }}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 55, Quality = 50 }},
                    new List<Item> {new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 55, Quality = 50 }}
                },
                // "Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
                // Quality increases by 2 when there are 10 days or less
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 6}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 9, Quality = 7}}

                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 9, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 8, Quality = 7}}
                },

                // and by 3 when there are 5 days or less
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 6, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 7}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 8}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 4, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 3, Quality = 8}}
                },
                // but Quality drops to 0 after the concert
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 1, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 8}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 0}}
                },
                new object[]
                {
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -1, Quality = 5 }},
                    new List<Item> {new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = -2, Quality = 0 }}
                },
                // Mutants
                new object[]
                {
                    new List<Item> {new Item {Name = "foo", SellIn = 1, Quality = 10}},
                    new List<Item> {new Item {Name = "foo", SellIn =0, Quality = 9}}
                },

            };
    }
}
