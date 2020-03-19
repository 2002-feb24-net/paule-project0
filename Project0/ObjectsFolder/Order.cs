using System;

namespace Objects
{
    class Order : ObjectParent
    {
        public string name {get; set;}
        public double price {get; set;}
        public Stock MyWrappedItem {get; set;}

        public Order(Stock x)
        {
            SetName(x.GetName());
            SetPrice(x.GetPrice());
            SetItem(x);
        }
        public void SetName(string x)
        {
            this.name = x;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetPrice(double x)
        {
            this.price = x;
        }

        public double GetPrice()
        {
            return this.price;
        }

        public void SetItem(Stock x)
        {
            this.MyWrappedItem = x;
        }

        public Stock GetItem()
        {
            return this.MyWrappedItem;
        }
    }
}