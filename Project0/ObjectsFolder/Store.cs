using System;
using Creators;
using System.Collections.Generic;
using SerDSer;
using Utility;
using Managers;

namespace Objects
{
    class Store : ObjectParent
    {
        public string location {get; set;}
        public string myPath {get; set;}
        private OrderManager MyOrderManager;

        private List<string> MyStoreTopics = new List<string>{};
        private Dictionary<string,List<Stock>> MyStock = new Dictionary<string,List<Stock>>{};

        StockCreator MyStockCreator = new StockCreator();
        public void Initialize()
        {
            var MyDeserializer = new Deserializer();
            MyStock = MyDeserializer.DeserializeStock(myPath);
            foreach (string key in MyStock.Keys)
            {
                if (key == "Back")
                {
                    break;
                }
                MyStoreTopics.Add(key);
            }
            List<Stock> ListOfStocks = new List<Stock>();
            MyStock[MyStoreTopics[0]] = ListOfStocks;
        }

        public void ReceiveOrderManager(OrderManager InputOrderManager)
        {
            this.MyOrderManager = InputOrderManager;
        }
        public void SetName(string x)
        {
            this.location = x;
        }

        public string GetName()
        {
            return this.location;
        }

        public Dictionary<string,List<Stock>> GetMyStock()
        {
            return this.MyStock;
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
            if (MyStock.ContainsKey(x.GetTopic()))
            {
                MyStock[myTopic].Add(x);
            }
            else
            {
                List<Stock> NewStockList = new List<Stock>();
                NewStockList.Add(x);
                MyStock.Add(myTopic,NewStockList);
                MyStoreTopics.Add(myTopic);
            }
        }

        public Dictionary<string,List<Stock>> GetStock()
        {
            return MyStock;
        }

        public void Serialize()
        {
            Serializer MySerializer = new Serializer();
            MySerializer.Serialize(myPath,MyStock);
        }

        public void DisplayGoods()
        {
            Console.WriteLine($"Welcome to the {GetName()} shop!"); 
            Console.WriteLine("All goods here are obtained organically and are hand picked by well-trained human-rights activists.");
            Console.WriteLine("What are you looking for?");
            Console.WriteLine();
            Console.WriteLine("Sections: ");

            for(int i = 0; i<MyStoreTopics.Count;i++)
            {
                Console.WriteLine($"{i+1}. " + MyStoreTopics[i] + $" (In Stock: {MyStock[MyStoreTopics[i]].Count})");
            }
            Console.WriteLine($"Enter 'B' or '0' to go back to Main Menu. Enter 'O' to see your current shopping cart.");
            Console.WriteLine();
            Console.Write("I'm interested in ");
        }

        public void PopulateChosenTopic(int x)
        {
            Console.Clear();
            Console.WriteLine($"You have chosen: {MyStoreTopics[x-1]}");
            List<Stock> activeList = MyStock[MyStoreTopics[x-1]];
            Stock activeItem;
            if (activeList.Count > 1)
            {
                for (int i = 1; i<activeList.Count;i++)
                {
                    activeItem = activeList[i];
                    Console.WriteLine($"{activeItem.GetName()}, {activeItem.GetDescription()}  ---- Price: {activeItem.GetPrice()}");
                }
            }
            else
            {
                Console.WriteLine("There are no items for sale in this topic!");
            }
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("Enter 'B' or '0' to go back. Enter 'O' to see your current shopping cart.");
            var tempInputCollector = new InputCollector();
            bool GoodNumber = false;
            int input = 0;
            while (!GoodNumber)
            {
                input = tempInputCollector.GetNumber();
                if (input == 0 || input == 9999)
                {
                    GoodNumber = true;
                }
                if (input > 0 && input < activeList.Count)
                {
                    GoodNumber = true;
                }
                if (GoodNumber == false)
                {
                    Console.WriteLine("That is not a choice!");
                }
            }
            if (input == 0)
            {
                return;
            }
            if (input == 9999)
            {
                // MyOrderManager.PeekOrder();
                Console.WriteLine("TODO: Not yet created.");
                Console.WriteLine("Press enter to continue."); //THIS IS TEMPORARY TO SEE MY OUTPUT
                Console.ReadLine(); //THIS IS TEMPORARY TO SEE MY OUTPUT
                return;
            } 
            Console.WriteLine($"{activeList[input-1].GetName()} has been added to your order. ");
            MyStock[MyStoreTopics[x-1]].RemoveAt(input-1);
        }

        public bool CheckTopicChoice(int x)
        {
            if (x > MyStoreTopics.Count)
            {
                return false;
            }
            return true;
        }
    }
}