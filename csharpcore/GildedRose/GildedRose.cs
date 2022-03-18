using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace GildedRoseKata
{
    public class GildedRose
    {
        private readonly IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            foreach (var item in Items)
            {
                UpdateQualityStuff(item);
            }
        }

        private void UpdateQualityStuff(Item item)
        {
            if (item.Name == "Aged Brie")
            {
                UpdateQuantityForBrie(item);
                return;
            }

            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                UpdateQuantityForBackstage(item);
                return;
            }

            if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                return;
            }

            UpdateQualityForOthers(item);
        }

        internal void UpdateQuantityForBackstage(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;

                if (item.SellIn < 11)
                    if (item.Quality < 50)
                        item.Quality++;

                if (item.SellIn < 6)
                    if (item.Quality < 50)
                        item.Quality++;
            }

            item.SellIn--;
            if (item.SellIn < 0)
                item.Quality = 0;
        }

        private static void UpdateQuantityForBrie(Item item)
        {
            if (item.Quality < 50)
                item.Quality++;

            item.SellIn--;
            if ((item.SellIn < 0) && (item.Quality < 50))
                item.Quality++;
        }


        private static void UpdateQualityForOthers(Item item)
        {
            if (item.Quality > 0)
                item.Quality--;

            item.SellIn--;
            if (item.SellIn < 0)
                if (item.Quality > 0)
                    item.Quality--;
        }
    }
}
