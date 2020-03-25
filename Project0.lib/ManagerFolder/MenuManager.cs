using System;
using Utility;

namespace Managers
{
        /// <summary>
        ///  This class handles the menu redirect logic, and some basic user interactions.
        /// </summary>
    public class MenuManager
    {
        private InputCollector MyInputCollector;
        private PersonManager MyPersonManager;
        private StoreManager MyStoreManager;
        private OrderManager MyOrderManager;

        /// <summary>
        ///  This constructor ensure manager consistency, and data access for this manager.
        /// </summary>
        public MenuManager(InputCollector MyInputCollector, PersonManager MyPersonManager, StoreManager MyStoreManager, OrderManager MyOrderManager)
        {
            this.MyInputCollector = MyInputCollector;
            this.MyPersonManager = MyPersonManager;
            this.MyStoreManager = MyStoreManager;
            this.MyOrderManager = MyOrderManager;
        }

        /// <summary>
        ///  This method sets the current user for all managers, and general consistency maintenance like initializing the other managers for each other.
        /// </summary>
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
                    MyOrderManager.GetUserHistory();
                    choice = 0;
                }
                else if (choice == 3)
                {
                    //account
                    WelcomeMessageMenu(3);
                    MyPersonManager.EditAccountDetails();
                    choice = 0;
                }
                else if (choice == 4)
                {
                    //admin
                    WelcomeMessageMenu(4);
                    if(MyPersonManager.CheckEmployee())
                    {
                        EmployeeManager MyEmployeeManager = new EmployeeManager(MyPersonManager,MyStoreManager,MyOrderManager,MyPersonManager.GetCurrentUser());
                        choice = MyEmployeeManager.EmployeeInitialize();
                    }
                    else
                    {
                        Console.WriteLine("Access denied! Please contact a system admin if you think this is an error. They can be reached at hr@revature.com");
                        Console.WriteLine("Press enter to continue.");
                        Console.ReadLine();
                    }
                    choice = 0;
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