using System;
using System.Collections.Generic;

namespace Objects
{
    public class Order : ObjectParent
    {
        private string name {get; set;}
        private double price {get; set;}
        private DateTime myDate {get; set;}
        private List<Stock> MyWrappedItems {get; set;}

        public Order()
        {
            SetDate(DateTime.Now);
            this.price = 0;
            MyWrappedItems = new List<Stock>();
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
            foreach (var item in MyWrappedItems)
            {
                this.price = this.price + item.GetPrice();
            }
            return this.price;
        }

        public void AddItem(Stock x)
        {
            this.MyWrappedItems.Add(x);
        }

        public void RemoveItem(Stock x)
        {
            this.MyWrappedItems.Remove(x);
        }

        public void SetItems(List<Stock> x)
        {
            this.MyWrappedItems = x;
        }

        public List<Stock> GetItems()
        {
            return this.MyWrappedItems;
        }

        public void SetDate(DateTime x)
        {
            this.myDate = x;
        }

        public DateTime GetDate()
        {
            return this.myDate;
        }
    }
}