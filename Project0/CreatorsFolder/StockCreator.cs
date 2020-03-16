using System;
using Objects;

namespace Creators
{
    class StockCreator
    {
        public Stock CreateStockItem(Store MyStore, string topic, string ShortDescription, string LongDescription, double Price)
        {
            Stock NewStock = new Stock();
            NewStock.SetName(ShortDescription);
            NewStock.SetPrice(Price);
            NewStock.SetDescription(LongDescription);
            NewStock.SetTopic(topic);
            return NewStock;
        }
    }
}