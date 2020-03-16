using System;
using Managers;
using Creators;
using System.Collections.Generic;

namespace Objects
{
    class Store : ObjectParent
    {
        private string location;
        private int locationInt;
        private List<string> MyStoreTopics = new List<string>{"Jewelry","Electronics (Non-Phone)","Purses","Wallets","Phones","Household Items","Cars","Gardening Tools","Back"};
        private Dictionary<string,List<Stock>> MyDictionary = new Dictionary<string,List<Stock>>{};
        private StockManager MyStockManager = new StockManager();
        private StockCreator MyStockCreator = new StockCreator();
        public Store()
        {
            GetStoreTopics();
        }
        public void SetName(string x)
        {
            this.location = x;
        }

        public string GetName()
        {
            return this.location;
        }

        public void SetNameAsInt(int x)
        {
            this.locationInt = x;
        }

        public int GetNameAsInt()
        {
            return locationInt;
        }

        private void GetStoreTopics()
        {
            foreach (string key in MyDictionary.Keys)
            {
                MyStoreTopics.Add(key);
            }
        }

        public void SetMyDictionary(Dictionary<string,List<Stock>> x)
        {
            MyDictionary = x;
        }

        public void AddStock(Stock x)
        {
            string myTopic = x.GetTopic();
            if (MyDictionary.ContainsKey(x.GetTopic()))
            {
                MyDictionary[myTopic].Add(x);
            }
            else
            {
                List<Stock> NewStockList = new List<Stock>();
                NewStockList.Add(x);
                MyDictionary.Add(myTopic,NewStockList);
                MyStoreTopics.Add(myTopic);
            }
        }
    }
}