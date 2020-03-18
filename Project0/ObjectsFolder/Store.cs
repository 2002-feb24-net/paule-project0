using System;
using Managers;
using Creators;
using System.Collections.Generic;

namespace Objects
{
    class Store : ObjectParent
    {
        public string location {get; set;}
        public string myPath {get; set;}

        private List<string> MyStoreTopics = new List<string>{"Jewelry","Electronics (Non-Phone)","Purses","Wallets","Phones","Household Items","Cars","Gardening Tools","Back"};
        private Dictionary<string,List<Stock>> MyDictionary = new Dictionary<string,List<Stock>>{};
        private StockManager MyStockManager;
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

        public void SetPath(string x)
        {
            this.myPath = x;
        }

        public string GetPath()
        {
            return this.myPath;
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