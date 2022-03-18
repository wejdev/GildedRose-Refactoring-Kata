using System.Collections.Generic;

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
                UpdateQualityByType(item);
            }
        }

        void UpdateQualityByType(Item item)
        {
            switch (item.Name)
            {
                case "Aged Brie":
                    UpdateQuantityForBrie(item);
                    return;
                case "Backstage passes to a TAFKAL80ETC concert":
                    UpdateQuantityForBackstage(item);
                    return;
                case "Sulfuras, Hand of Ragnaros":
                    return;
                case "Conjured":
                    UpdateQuantityForConjured(item);
                    return;
                default:
                    UpdateQualityForOthers(item);
                    break;
            }
        }

        private void UpdateQuantityForBackstage(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality++;

                if (item.SellIn < 11 && item.Quality < 50)
                    item.Quality++;

                if (item.SellIn < 6 && item.Quality < 50)
                    item.Quality++;
            }

            item.SellIn--;
            if (item.SellIn < 0)
                item.Quality = 0;
        }

        private void UpdateQuantityForBrie(Item item)
        {
            if (item.Quality < 50)
                item.Quality++;

            item.SellIn--;
            if ((item.SellIn < 0) && (item.Quality < 50))
                item.Quality++;
        }


        private void UpdateQualityForOthers(Item item)
        {
            UpdateQuantityByRate(item, 1);
        }

        private void UpdateQuantityForConjured(Item item)
        {
            UpdateQuantityByRate(item, 2);
        }

        private void UpdateQuantityByRate(Item item, int rate)
        {
            if (item.Quality > 0)
                item.Quality -= rate;

            item.SellIn--;
            if (item.SellIn < 0 && item.Quality > 0)
                item.Quality -= rate;
        }
    }
}
