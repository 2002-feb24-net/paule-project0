using System;
using Utility;
using Objects;
using System.Collections.Generic;
using SaveLoad;

namespace Managers
{
        /// <summary>
        ///  This class acts as the admin controls when a user (who has the employee tag) trys to access the admin menu.
        /// </summary>
    public class EmployeeManager
    {
        private InputCollector MyInputCollector = new InputCollector();
        private PersonManager MyPersonManager;
        private StoreManager MyStoreManager;
        private OrderManager MyOrderManager;
        private Person MyCurrentUser;
        private MakeNewUser MyNewUserInput;
        /// <summary>
        ///  This constructor gets the central managers from the hivemind upon creation. This is very important for data consistency.
        ///  It also creates a usercreator class which isnt used until much later. It is made here because it was more convenient to feed in the values here.
        /// </summary>
        public EmployeeManager(PersonManager MyPersonManager, StoreManager MyStoreManager, OrderManager MyOrderManager, Person MyCurrentUser)
        {
            this.MyPersonManager = MyPersonManager;
            this.MyStoreManager = MyStoreManager;
            this.MyOrderManager = MyOrderManager;
            this.MyCurrentUser = MyCurrentUser;
            MyNewUserInput = new MakeNewUser(MyPersonManager, MyStoreManager);
        }

        /// <summary>
        ///  This is an alternate constructor that does the same thing. It is just to make order matter a little less.!-- Same function, could add more if scaling makes it necessary.
        /// </summary>
        public EmployeeManager(Person MyCurrentUser, PersonManager MyPersonManager, StoreManager MyStoreManager, OrderManager MyOrderManager)
        {
            this.MyPersonManager = MyPersonManager;
            this.MyStoreManager = MyStoreManager;
            this.MyOrderManager = MyOrderManager;
            this.MyCurrentUser = MyCurrentUser;
            MyNewUserInput = new MakeNewUser(MyPersonManager, MyStoreManager);
        }

        /// <summary>
        ///  This method is the first contact with the user when inside of the admin menu option. It displays the 
        /// admin options, and then gets the user's input on which they want to do. B can also be used to break out of this method early. 
        /// If not exited early, calls a method that sorts through the methods to do the appropriate action. Returns to main menu when exited early,
        ///  or after all related logic methods.
        /// </summary>
        public int EmployeeInitialize()
        {
            Console.WriteLine("Current status: ADMIN");
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            List<string> MyAdminOptions = new List<string> {"Edit Users","Edit Locations","Lookup Order History"};
            for (int i = 0; i<MyAdminOptions.Count;i++)
            {
                Console.WriteLine($"{i+1}. {MyAdminOptions[i]}");
            }
            Console.WriteLine($"Enter 'B' or '0' to go back to Main Menu.");
            int input = 0;
            bool GoodNumber = false;
            while (!GoodNumber)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > MyAdminOptions.Count))
                {
                    GoodNumber = true;
                }
                if (GoodNumber == false)
                {
                    Console.WriteLine("That number is not on the list!");
                }
            }
            return ChooseEmployeeOption(input);
        }

        /// <summary>
        ///  This sorts the result of the initial admin options quary. Notice it is private and can only be called internal.
        /// Redirects to appropriate method. Returns to main menu after a line of internal logic methods.
        /// </summary>
        private int ChooseEmployeeOption(int x)
        {
            if (x == 0)
            {
                return 0;
            }
            else if (x == 1)
            {
                EditUsers();
            }
            else if (x == 2)
            {
                EditLocations();
            }
            return 4;
        }

        /// <summary>
        ///  This method represents the admin's ability to edit any users information. Prints all users, and lets the admin deside which to edit.
        /// Passes selected user to a method which will handle the editing. Internal method only.
        /// </summary>
        private void EditUsers()
        {
            Console.Clear();
            Console.WriteLine("Which user would you like to edit?");
            Console.WriteLine();
            Dictionary<string,Person> MyManagedPeople = MyPersonManager.GetManagedPeople();
            List<string> MyNames = MyPersonManager.GetManagedPeopleNames();
            Person MyCurrentPerson;
            for (int i = 0; i<MyNames.Count;i++)
            {
                MyCurrentPerson = MyManagedPeople[MyNames[i]];
                Console.WriteLine($"{i+1}. User: {MyCurrentPerson.GetName()}, Location: {MyCurrentPerson.GetLocation()}, Employee: {MyCurrentPerson.GetEmployeeTag()}, OrderCount: {MyOrderManager.GetOrderCount(MyCurrentPerson.GetName())}");
            }
            Console.WriteLine($"Enter 'B' or '0' to go back to Admin Menu.");
            bool GoodChoice = false;
            int input = 0;
            while (!GoodChoice)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > MyNames.Count))
                {
                    GoodChoice = true;
                }
                if (GoodChoice == false)
                {
                    Console.WriteLine("That number is not on the list!");
                }
            }
            if (input == 0)
            {
                return;
            }
            MyCurrentPerson = MyManagedPeople[MyNames[input-1]];
            InProcessEditingUser(MyCurrentPerson);
            return;
        }

        /// <summary>
        ///  This method represents the admin editing a selected user. Prints all the info about the user and asks the admin what about the user they would like to change.
        /// If the admin picks the username,location, or password this method calls a cooresponding method inside of our user build to make a new username,location, or password.
        /// If the admin picks the employee tag, it is changed using a setter through the person manager. End of internal logic line. Returns to admin menu. 
        /// Note: admin employee status can not be removed.
        /// </summary>
        private void InProcessEditingUser(Person MyCurrentPerson)
        {
            Console.Clear();
            Console.WriteLine($"Currently Selected User: {MyCurrentPerson.GetName()}");
            Console.WriteLine($"Current Location: {MyCurrentPerson.GetLocation()}");
            Console.WriteLine($"Current Password: {MyCurrentPerson.GetPassword()}");
            Console.WriteLine($"Employee: {MyCurrentPerson.GetEmployeeTag()}");
            Console.WriteLine("What would you like to change?");
            List<string> EmployeeUserChangeOptions = new List <string> {"Change Username","Change Location", "Change Password", "Change Employee Status"};
            Console.WriteLine ("");
            for (int i = 0; i<EmployeeUserChangeOptions.Count; i++)
            {
                Console.WriteLine($"{i+1}. {EmployeeUserChangeOptions[i]}");
            }
            Console.WriteLine($"Enter 'B' or '0' to go back to Admin Menu.");
            Console.WriteLine();

            bool GoodChoice = false;
            int input = 0;
            while (!GoodChoice)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > EmployeeUserChangeOptions.Count))
                {
                    GoodChoice = true;
                }
                if (GoodChoice == false)
                {
                    Console.WriteLine("That number is not on the list!");
                }
            }
            if (input == 0)
            {
                return;
            }
            else if (input == 1)
            {
                MyCurrentPerson.SetName(MyNewUserInput.GetDesiredUsername());
                Console.WriteLine("Username set.");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            else if (input == 2)
            {
                MyCurrentPerson.SetLocation(MyNewUserInput.GetDesiredLocation());
                Console.WriteLine("Location set.");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            else if (input == 3)
            {
                MyCurrentPerson.SetPassword(MyNewUserInput.GetDesiredPassword());
                Console.WriteLine("Password set.");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            else if (input == 4)
            {
                bool IsEmployee = MyCurrentPerson.GetEmployeeTag();
                if (IsEmployee && MyCurrentPerson.GetName() != "admin")
                {
                    MyCurrentPerson.SetEmployeeTag(false);
                    Console.WriteLine($"{MyCurrentPerson.GetName()} is no longer an employee.");
                }
                else if (!IsEmployee && MyCurrentPerson.GetName() != "admin")
                {
                    MyCurrentPerson.SetEmployeeTag(true);
                    Console.WriteLine($"{MyCurrentPerson.GetName()} is now an employee.");
                }
                else
                {
                    Console.WriteLine("Admin can never be removed from staff.");
                }
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            return;
        }
        /// <summary>
        ///  This method represent an admins choice to edit locations. Prints the locations, and gives an option to add a new location.
        /// Can break out of method early. If admin choose to make a new location, creates one on the spot and then creates a Save object to handle putting it in the database.
        /// Saver returns if the operation was a success. If admin choose one of the locations, calls a method (and inputs location) to edit the chosen location.
        /// </summary>
        private void EditLocations()
        {
            Console.Clear();
            Console.WriteLine("Which Location would you like to edit?");
            Console.WriteLine();
            Dictionary<string,Store> MyManagedStores = MyStoreManager.GetManagedStores();
            List<string> MyLocationNames = MyStoreManager.GetManagedLocationNames();
            Store MyCurrentStore;
            for (int i = 0; i<MyLocationNames.Count;i++)
            {
                MyCurrentStore = MyManagedStores[MyLocationNames[i]];
                Console.WriteLine($"{i+1}. Location: {MyCurrentStore.GetName()}, Total Stock: {MyCurrentStore.GetTotalStock()}");
            }
            Console.WriteLine($"{MyLocationNames.Count+1}. Add New Location");
            Console.WriteLine($"Enter 'B' or '0' to go back to Admin Menu.");
            bool GoodChoice = false;
            int input = 0;
            while (!GoodChoice)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > MyLocationNames.Count+1))
                {
                    GoodChoice = true;
                }
                if (GoodChoice == false)
                {
                    Console.WriteLine("That number is not on the list!");
                }
            }
            Console.WriteLine($"Picked: {input} Count: {MyLocationNames.Count}");
            if (input == 0)
            {
                return;
            }
            if (input == MyLocationNames.Count+1)
            {
                Console.Clear();
                Console.WriteLine("What is the name of your new location?");
                string inputCity = MyInputCollector.InputCity();
                string inputState = MyInputCollector.InputState();
                string x = inputCity + "-" + inputState;

                Save MySave = new Save();
                bool worked = MySave.CreateLocation(x);

                if(worked)
                {
                    Console.Clear();
                    Console.WriteLine($"Location {x} successfully created.");
                    Console.WriteLine("Please log out and back in to properly generate the data related to your new location.");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Location creation failed.");
                    Console.WriteLine($"Location {x} either already exists, or there is a problem accessing the database.");
                    Console.WriteLine("Please contact your local systems admin.");
                    Console.WriteLine();
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                }
                return;
            }
            MyCurrentStore = MyManagedStores[MyLocationNames[input-1]];
            InProcessEditingLocation(MyCurrentStore);
            return;
        }

        /// <summary>
        ///  This method represents the admin editing a specific location. Prints the options, and lets the admin pick from them, calling the appropriate methods
        /// related to their logic. Can be exited early to go back to the admin menu. 
        /// </summary>
        private void InProcessEditingLocation(Store MyCurrentStore)
        {
            Console.Clear();
            Console.WriteLine($"Currently selected: {MyCurrentStore.GetName()}");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            List<string> options = new List<string> {"ADD_STOCK_ITEM","REMOVE_STOCK_ITEM"};
            for (int i = 0; i<options.Count;i++)
            {
                Console.WriteLine($"{i+1}. {options[i]}");
            }
            Console.WriteLine($"Enter 'B' or '0' to go back to Admin Menu.");
            Console.WriteLine();
            bool GoodChoice = false;
            int input = 0;
            while (!GoodChoice)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > options.Count))
                {
                    GoodChoice = true;
                }
                if (GoodChoice == false)
                {
                    Console.WriteLine("That number is not on the list!");
                }
            }
            if (input == 0)
            {
                return;
            }
            if (input == 1)
            {
                AdminAddStock(MyCurrentStore);
            }
            if (input == 2)
            {
                AdminRemoveStock(MyCurrentStore);
            }
        }

        /// <summary>
        ///  This method represents when the admin decides to add stock to a specific store. Must fill all fields of the stock item. 
        /// Adds the stock to the specific store, and gives the option to add more stock. Returns to admin menu when exited early, or when finished.
        /// End of internal logic line.
        /// </summary>
        private void AdminAddStock(Store MyCurrentStore)
        {
            bool finished = false;
            while (!finished)
            {
                Console.Clear();
                Console.WriteLine($"You are now adding stock for {MyCurrentStore.GetName()}");
                Console.WriteLine();

                Console.Write("Stock Name: ");
                string StockName = Console.ReadLine();

                Console.Clear();
                Console.WriteLine($"Name:{StockName}");
                Console.WriteLine();
                Console.WriteLine("If you do not pick an existing topic here, the program will crash on save.");
                Console.Write("Stock topic: ");
                string StockTopic = Console.ReadLine();

                bool GoodNumber = false;
                string StockPriceString = "";
                double StockPrice = 0;
                while (!GoodNumber)
                {
                    Console.Clear();
                    Console.WriteLine($"Name:{StockName}, Topic:{StockTopic}");
                    Console.WriteLine();
                    Console.Write("Stock price: ");
                    StockPriceString = Console.ReadLine();

                    try
                    {
                        StockPrice = Convert.ToDouble(StockPriceString);
                        GoodNumber = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("You must enter a number!");
                    }
                }

                Console.Clear();
                Console.WriteLine($"Name:{StockName}, Topic:{StockTopic}, Price:{StockPrice}");
                Console.WriteLine();
                Console.Write("Description: ");
                string StockDescription = Console.ReadLine();

                Stock x = new Stock();
                x.SetDescription(StockDescription);
                x.SetName(StockName);
                x.SetPrice(StockPrice);
                x.SetTopic(StockTopic);


                MyCurrentStore.AddStock(x);
                Console.WriteLine($"Added {StockTopic}:{StockName}:{StockPrice}:{StockDescription}");
                Console.WriteLine();
                Console.WriteLine("Do you want to add more stock? y/n");
                string q = Console.ReadLine();
                if (q.ToLower() == "y")
                {

                }
                else if (q.ToLower() == "n")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("I'll take that as a no.");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    return;
                }
            }
        }

        /// <summary>
        ///  This method represents the admin removing stock from a specific store.
        /// Prints all stock in all topics, and the admin picks which they wish to remove.
        /// This method unofficially links a collection of lists to print them and then remove the correct one after.
        /// Can continue to remove if admin wants to. Returns to admin menu when finished, or when exited early. End of internal logic line.
        /// </summary>
        private void AdminRemoveStock(Store MyCurrentStore)
        {
            Dictionary<string,List<Stock>> MyDictionary = MyCurrentStore.GetStock();
            bool finished = false;
            while (!finished)
            {
                Console.Clear();
                Console.WriteLine($"You are now removing stock from {MyCurrentStore.GetName()}");
                Console.WriteLine();
                Console.WriteLine("Which item would you like to remove?");
                Console.WriteLine();
                int total = 0;
                int c = 0;
                foreach (var val in MyDictionary)
                {
                    total = total + val.Value.Count;
                    int ic = c;
                    for (int i = 0; ic+i<total; i++)
                    {
                        Console.WriteLine($"{c+1}. {val.Value[i].GetName()} -- {val.Value[i].GetPrice()} ---- {val.Value[i].GetDescription()}");
                        c++;
                    }
                }
                Console.WriteLine($"Enter 'B' or '0' to go back to Admin Menu.");
                Console.WriteLine();
                bool GoodChoice = false;
                int input = 0;
                while (!GoodChoice)
                {
                    input = MyInputCollector.GetNumber();
                    if (!(input < 0 || input > total))
                    {
                        GoodChoice = true;
                    }
                    if (GoodChoice == false)
                    {
                        Console.WriteLine("That number is not on the list!");
                    }
                }
                if (input == 0)
                {
                    return;
                }
                Console.Clear();
                Console.WriteLine();
                foreach (var val in MyDictionary)
                {
                    if(input > val.Value.Count)
                    {
                        input = input - val.Value.Count;
                    }
                    else
                    {
                        Console.WriteLine($"{val.Value[input-1].GetName()} -- {val.Value[input-1].GetPrice()} ---- {val.Value[input-1].GetDescription()} removed.");
                        val.Value.RemoveAt(input-1);
                        Console.WriteLine();
                        break;
                    }
                }
                Console.WriteLine("Would you like to remove another item? y/n");
                string q = Console.ReadLine();
                if (q.ToLower() == "y")
                {

                }
                else if (q.ToLower() == "n")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("I'll take that as a no.");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    return;
                }
            }
        }
    }
}