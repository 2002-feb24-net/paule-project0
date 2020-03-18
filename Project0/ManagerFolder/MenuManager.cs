using System;
using Utility;

namespace Managers
{
    class MenuManager
    {
        private InputCollector MyInputCollector = new InputCollector();
        private PersonManager MyPersonManager = new PersonManager();
        private StoreManager MyStoreManager = new StoreManager();
        private OrderManager MyOrderManager = new OrderManager();

        public MenuManager(InputCollector MyInputCollector, PersonManager MyPersonManager, StoreManager MyStoreManager, OrderManager MyOrderManager)
        {
            this.MyInputCollector = MyInputCollector;
            this.MyPersonManager = MyPersonManager;
            this.MyStoreManager = MyStoreManager;
            this.MyOrderManager = MyOrderManager;
        }
        public void InitializeMainMenu(int choice = 0)
        {
            Console.Clear();
            bool finished = false;
            while (!finished)
            {
                if(choice == 0)
                {
                    //main
                    WelcomeMessageMenu(0);
                    choice = MyInputCollector.ChooseMenu(0);
                }
                else if (choice == 1)
                {
                    //shop
                    WelcomeMessageMenu(1);
                    MyStoreManager.Initialize();
                    bool GoodNumber = false;
                    int shoptopicchoice = 0;
                    while (!GoodNumber)
                    {
                        shoptopicchoice = MyInputCollector.GetNumber();
                        GoodNumber = MyStoreManager.CheckTopicChoice(shoptopicchoice);
                        if (GoodNumber == false)
                        {
                            Console.WriteLine("That is not a choice!");
                        }
                    }
                    choice = MyStoreManager.ShopTopicChoice(shoptopicchoice);
                }
                else if (choice == 2)
                {
                    //history
                    WelcomeMessageMenu(2);
                    // TODO MyOrderManager.GetUserHistory();
                    Console.WriteLine("TODO: Not yet created.");
                }
                else if (choice == 3)
                {
                    //account
                    WelcomeMessageMenu(3);
                    // TODO MyPersonManager.EditAccountDetails();
                    Console.WriteLine("TODO: Not yet created.");
                }
                else if (choice == 4)
                {
                    //admin
                    WelcomeMessageMenu(4);
                    Console.WriteLine("TODO: Not yet created.");
                    // if(MyPersonManager.CheckEmployee())
                    // {
                    //     TODO MyStoreManager.EmployeeInitialize();
                    // }
                }
                else if (choice == 5)
                {
                    //quit
                    WelcomeMessageMenu(5);
                    finished = true;
                }
                else
                {
                    Console.WriteLine("That is not a choice!");
                }
            }
        }

        private void WelcomeMessageMenu(int x)
        {
            Console.Clear();
            if (x == 0)
            {
                Console.WriteLine("You have been redirected to: MAIN MENU");
            }
            else if (x == 1)
            {
                Console.WriteLine("You have been redirected to: SHOP");
            }
            else if (x == 2)
            {
                Console.WriteLine("You have been redirected to: HISTORY");
            }
            else if (x == 3)
            {
                Console.WriteLine("You have been redirected to: ACCOUNT");
            }
            else if (x == 4)
            {
                Console.WriteLine("You have been redirected to: ADMIN");
            }
            else if (x == 5)
            {
                Console.WriteLine("Stop by again soon!");
                Console.WriteLine("We get new items all the time.");
            }
        }
    }
}