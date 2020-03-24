using System;
using Utility;
using Objects;
using System.Collections.Generic;

namespace Managers
{
    public class EmployeeManager
    {
        private InputCollector MyInputCollector = new InputCollector();
        private PersonManager MyPersonManager;
        private StoreManager MyStoreManager;
        private OrderManager MyOrderManager;
        private Person MyCurrentUser;
        private MakeNewUser MyNewUserInput;

        public EmployeeManager(PersonManager MyPersonManager, StoreManager MyStoreManager, OrderManager MyOrderManager, Person MyCurrentUser)
        {
            this.MyPersonManager = MyPersonManager;
            this.MyStoreManager = MyStoreManager;
            this.MyOrderManager = MyOrderManager;
            this.MyCurrentUser = MyCurrentUser;
            MyNewUserInput = new MakeNewUser(MyPersonManager, MyStoreManager);
        }

        public EmployeeManager(Person MyCurrentUser, PersonManager MyPersonManager, StoreManager MyStoreManager, OrderManager MyOrderManager)
        {
            this.MyPersonManager = MyPersonManager;
            this.MyStoreManager = MyStoreManager;
            this.MyOrderManager = MyOrderManager;
            this.MyCurrentUser = MyCurrentUser;
            MyNewUserInput = new MakeNewUser(MyPersonManager, MyStoreManager);
        }

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
            }
            return ChooseEmployeeOption(input);
        }

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
            }
            if (input == 0)
            {
                return;
            }
            MyCurrentPerson = MyManagedPeople[MyNames[input-1]];
            InProcessEditingUser(MyCurrentPerson);
            return;
        }

        public void InProcessEditingUser(Person MyCurrentPerson)
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

            bool GoodChoice = false;
            int input = 0;
            while (!GoodChoice)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > EmployeeUserChangeOptions.Count))
                {
                    GoodChoice = true;
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

        public void EditLocations()
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
            Console.WriteLine($"{MyLocationNames.Count}. Add New Location");
            Console.WriteLine($"Enter 'B' or '0' to go back to Admin Menu.");
            bool GoodChoice = false;
            int input = 0;
            while (!GoodChoice)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > MyLocationNames.Count))
                {
                    GoodChoice = true;
                }
            }
            if (input == 0)
            {
                return;
            }
            if (input == MyLocationNames.Count)
            {
                Console.Clear();
                Console.WriteLine("What is the name of your new location?");
                string inputCity = MyInputCollector.InputCity();
                var NewStore = new Store();
                NewStore.SetName(inputCity);
                List<Stock> ListOfStocks = new List<Stock>();

                Console.WriteLine(" TODO: CURRENTLY UNINMPLIMENTED");
                Console.WriteLine("PRESS ENTER TO CONTINUE");
                Console.ReadLine();
            }
            MyCurrentStore = MyManagedStores[MyLocationNames[input-1]];
            //InProcessEditingLocation(MyCurrentStore);
            return;
        }
    }
}