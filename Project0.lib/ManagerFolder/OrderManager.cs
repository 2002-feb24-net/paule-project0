using System;
using System.Collections.Generic;
using Objects;
using Utility;
using SaveLoad;

namespace Managers
{
    public class OrderManager : IManagerParent
    {
        private Dictionary<string,List<Order>> MyManagedOrders = new Dictionary<string,List<Order>>();
        private Order MyCurrentOrder = new Order();
        private MainManager MyMainManager;
        private static int OrdersManaged = 0;
        private string CurrentUser = null;

        public Order GetCurrentOrder()
        {
            return MyCurrentOrder;
        }

        public OrderManager(MainManager x)
        {
            this.MyMainManager = x;
        }
        public override int GetTotal()
        {
            OrdersManaged = MyManagedOrders.Count;
            return OrdersManaged;
        }

        public void PullData()
        {
            var MyDataPuller = new Load();
            MyManagedOrders = MyDataPuller.PullOrderDictionary();
        }

        public override bool CheckFor(string x)
        {
            if(MyManagedOrders.ContainsKey(x))
            {
                return true;
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyManagedOrders[x];
        }

        public override void SetCurrent(string x)
        {
            CurrentUser = x;
        }

        public void AddToCurrentOrder(Stock x)
        {
            if (MyCurrentOrder == null)
            {
                MyCurrentOrder = new Order();
            }
            MyCurrentOrder.SetName(CurrentUser);
            MyCurrentOrder.AddItem(x);
        }

        public void GetUserHistory()
        {
            List<Order> MyOrderHistory;
            try
            {
                MyOrderHistory = MyManagedOrders[CurrentUser];
            }
            catch (Exception)
            {
                Console.WriteLine("You do not have any placed orders!");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("You have been redirected to: ORDERHISTORY");
            Console.WriteLine();
            Console.WriteLine("Which Order would you like to look at?");
            for (int i = 0; i<MyOrderHistory.Count;i++)
            {
                Console.WriteLine($"{i+1}. {MyOrderHistory[i].GetDate()}");
            }
            Console.WriteLine($"Enter 'B' or '0' to go back to Main Menu.");
            Console.WriteLine();
            var myInputCollector = new InputCollector();
            int choice = myInputCollector.GetNumber();
            bool GoodChoice = false;
            while (!GoodChoice)
            {
                if (!(choice < 0 || choice > MyOrderHistory.Count))
                {
                    GoodChoice = true;
                }
                if (GoodChoice == false)
                {
                    Console.WriteLine("That number is not on the list!");
                }
            }
            if (choice == 0)
            {
                return;
            }
            ShowChosenHistory(MyOrderHistory[choice-1]);
        }
        
        private void ShowChosenHistory(Order x)
        {
            Console.Clear();
            Console.WriteLine($"Displaying {x.GetName()}'s order from {x.GetDate()}.");
            List<Stock> MyCurrentStock = x.GetItems();
            for (int i = 0; i<x.GetItems().Count;i++)
            {
                Console.WriteLine($"Item {i+1}: {MyCurrentStock[i].GetName()}, {MyCurrentStock[i].GetPrice()} , {MyCurrentStock[i].GetDescription()}");
            }
            Console.WriteLine();
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        public int GetOrderCount(string x)
        {
            int y = 0;
            try
            {
                y = MyManagedOrders[x].Count;
            }
            catch (Exception)
            {
                //I honestly dont know what to do with this. It doesnt matter if its an error, just need it to not crash.
            }
            return y;
        }

        public int PeekOrder()
        {
            Console.Clear();
            Console.WriteLine("This is your current order: ");
            Console.WriteLine();
            for (int i = 0; i<MyCurrentOrder.GetItems().Count;i++)
            {
                Console.WriteLine($"{MyCurrentOrder.GetItems()[i].GetName()} -- {MyCurrentOrder.GetItems()[i].GetDescription()} ------- {MyCurrentOrder.GetItems()[i].GetPrice()}");
            }
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            List<string> MyOptions = new List<string> {"PAY","REMOVE-ITEM"};
            for (int i = 0; i<MyOptions.Count;i++)
            {
                Console.WriteLine($"{i+1}. {MyOptions[i]}");
            }
            Console.WriteLine("Enter 'B' or '0' to go back.");

            var tempInputCollector = new InputCollector();
            bool GoodNumber = false;
            int input = 0;
            while (!GoodNumber)
            {
                input = tempInputCollector.GetNumber();
                if (input == 0)
                {
                    GoodNumber = true;
                }
                if (input > 0 && input <= MyOptions.Count)
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
            if (input == 1)
            {
                Console.WriteLine("Thank you for your purchase!");
                if (MyManagedOrders.ContainsKey(CurrentUser))
                {
                    MyManagedOrders[CurrentUser].Add(MyCurrentOrder);
                }
                else
                {
                    List<Order> MyNewOrderList = new List<Order>();
                    MyManagedOrders.Add(CurrentUser,MyNewOrderList);
                    MyManagedOrders[CurrentUser].Add(MyCurrentOrder);
                }
                return -1;
            }
            if (input == 2)
            {
                Console.Clear();
                Console.WriteLine();
                if (MyCurrentOrder.GetItems().Count == 0)
                {
                    Console.WriteLine("There are no items in your order!");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    return 0;
                }
                Console.WriteLine("Which item would you like to remove?");
                Console.WriteLine();
                for (int i = 0; i<MyCurrentOrder.GetItems().Count;i++)
                {
                    Console.WriteLine($"{i+1}. {MyCurrentOrder.GetItems()[i].GetName()} -- {MyCurrentOrder.GetItems()[i].GetDescription()} ------- {MyCurrentOrder.GetItems()[i].GetPrice()}");
                }
                Console.WriteLine();
                bool GoodNumber2 = false;
                int input2 = 0;
                while (!GoodNumber2)
                {
                    input2 = tempInputCollector.GetNumber();
                    if (input2 == 0)
                    {
                        GoodNumber2 = true;
                    }
                    if (input2 > 0 && input2 <= MyCurrentOrder.GetItems().Count)
                    {
                        GoodNumber2 = true;
                    }
                    if (GoodNumber2 == false)
                    {
                        Console.WriteLine("That is not a choice!");
                    }
                }
                Save MySaver = new Save();
                MySaver.SwapToStore(MyCurrentOrder.GetItems()[input2-1].GetName());
                Console.WriteLine($"Removed {MyCurrentOrder.GetItems()[input2-1].GetName()}");
                MyMainManager.TossBackItem(MyCurrentOrder.GetItems()[input2-1]);
                MyCurrentOrder.RemoveItem(MyCurrentOrder.GetItems()[input2-1]);
            }
            return 0;
        }

        public Dictionary<string,List<Order>> GetManagedOrders()
        {
            return MyManagedOrders;
        }
    }
}