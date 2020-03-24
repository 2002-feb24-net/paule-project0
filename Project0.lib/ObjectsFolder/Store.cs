using System;
using System.Collections.Generic;
using Utility;
using Managers;
using SaveLoad;

namespace Objects
{
    public class Store : ObjectParent
    {
        public string location {get; set;}
        private OrderManager MyOrderManager;

        private List<string> MyStoreTopics = new List<string>{};
        private Dictionary<string,List<Stock>> MyStock = new Dictionary<string,List<Stock>>{};

        public void Initialize()
        {

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

        public void SetMyStock(Dictionary<string,List<Stock>> MyStock)
        {
            this.MyStock = MyStock;
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

        public int PopulateChosenTopic(int x)
        {
            Console.Clear();
            Console.WriteLine($"You have chosen: {MyStoreTopics[x-1]}");
            List<Stock> activeList = MyStock[MyStoreTopics[x-1]];
            Stock activeItem;
            if (activeList.Count > 0)
            {
                Console.WriteLine();
                for (int i = 0; i<activeList.Count;i++)
                {
                    activeItem = activeList[i];
                    Console.WriteLine($"{i+1}. {activeItem.GetName()} --- {activeItem.GetDescription()}  ---- Price: {activeItem.GetPrice()}");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("There are no items for sale in this topic!");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Which item would you like to buy?");
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
                if (input > 0 && input <= activeList.Count)
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
                return 0;
            }
            if (input == 9999)
            {
                int quitint = MyOrderManager.PeekOrder();
                if (quitint == -1)
                {
                    return -1;
                }
                return 0;
            } 
            Save MySaver = new Save();
            MySaver.SwapToOrder(activeList[input-1].GetName());
            Console.WriteLine($"{activeList[input-1].GetName()} has been added to your order. ");
            MyOrderManager.AddToCurrentOrder(MyStock[MyStoreTopics[x-1]][input-1]);
            MyStock[MyStoreTopics[x-1]].RemoveAt(input-1);
            Console.WriteLine();
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
            return 0;
        }

        public bool CheckTopicChoice(int x)
        {
            if (x > MyStoreTopics.Count)
            {
                return false;
            }
            return true;
        }

        public double GetTotalStock()
        {
            int x = 0;
            foreach (KeyValuePair<string,List<Stock>> key in MyStock)
            {
                x = x + key.Value.Count;
            }
            return x;
        }
    }
}