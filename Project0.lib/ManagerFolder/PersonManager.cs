using System;
using System.Collections.Generic;
using Objects;
using SaveLoad;
using Utility;
// using DataBaseTargets.lib.Model;

namespace Managers
{
    public class PersonManager : IManagerParent
    {
        private Dictionary<string,Person> MyManagedPeople = new Dictionary<string,Person>();
        private List<string> MyPeople = new List<string>{};
        private StoreManager MyStoreManager;
        private static int PersonsManaged = 0;
        private Person CurrentUser = null;

        public PersonManager()
        {
        }

        public void PullData()
        {
            var MyDataPuller = new Load();
            MyManagedPeople = MyDataPuller.PullPeopleDictionary();
            foreach (var val in MyManagedPeople)
            {
                MyPeople.Add(val.Key);
            }
        }

        public void ReceiveStoreManager(StoreManager x)
        {
            this.MyStoreManager = x;
        }
        
        public override int GetTotal()
        {
            return PersonsManaged;
        }

        public override bool CheckFor(string x)
        {
            if (x.ToUpper() == "SIGNUP")
            {
                return false;
            }
            
            foreach (var val in MyPeople)
            {
                if(val == x)
                {
                    return true;
                }
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyManagedPeople[x];
        }

        public void AddPerson(string username,string location,string password,bool employee)
        {
            Person x = new Person();
            x.SetName(username);
            x.SetEmployeeTag(employee);
            x.SetLocation(location);
            x.SetPassword(password);
            MyPeople.Add(x.GetName());
            MyManagedPeople.Add(x.GetName(),x);
        }

        public void SetCurrentUser(Person x)
        {
            CurrentUser = x;
        }

        public Person GetCurrentUser()
        {
            return CurrentUser;
        }
        
        public bool CheckCurrentPassword(string x)
        {
            //Console.WriteLine("Checking {0} vs {1}",x,CurrentUser.GetPassword());
            if (CurrentUser.GetPassword() == "")
            {
                Console.WriteLine("Critical error. No assigned user.");
                return false;
            }
            if (CurrentUser.GetPassword() == x)
            {
                Console.Clear();
                return true;
            }
            Console.Clear();
            Console.WriteLine("Incorrect Password. Please try again.");
            return false;
        }

        public Person GetUser(string x)
        {
            foreach (var val in MyManagedPeople)
            {
                if(val.Key == x)
                {
                    return (Person) MyManagedPeople[x];
                }
            }
            Console.WriteLine("Critical error: No user selected.");
            return null;
        }

        public override void SetCurrent(string x)
        {
            CurrentUser = MyManagedPeople[x];
        }

        public void EditAccountDetails()
        {
            Console.Clear();
            Console.WriteLine("You have been redirected to ACCOUNT_DETAILS");
            Console.WriteLine();
            Console.WriteLine($"User = {CurrentUser.GetName()}, Location = {CurrentUser.GetLocation()}");
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            List<string> Options = new List<string> {"Change Username","Change Location","Change Password"};
            for (int i = 0; i<Options.Count;i++)
            {
                Console.WriteLine($"{i+1}. {Options[i]}");
            }
            Console.WriteLine($"Enter 'B' or '0' to go back to Main Menu.");
            var MyInputCollector = new InputCollector();
            int input = 0;
            bool GoodNumber = false;
            while(!GoodNumber)
            {
                input = MyInputCollector.GetNumber();
                if (!(input < 0 || input > Options.Count))
                {
                    GoodNumber = true;
                }
                if (GoodNumber == false)
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
                var NewUserName = new MakeNewUser(this,MyStoreManager);
                string x = NewUserName.GetDesiredUsername();
                CurrentUser.SetName(x);
                Console.WriteLine($"Username changed to {x}");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                EditAccountDetails();
            }
            else if (input == 2)
            {
                var NewUserName = new MakeNewUser(this,MyStoreManager);
                string x = NewUserName.GetDesiredLocation();
                CurrentUser.SetLocation(x);
                Console.WriteLine($"Location changed to {x}");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                EditAccountDetails();
            }
            else if (input == 3)
            {
                var NewUserName = new MakeNewUser(this,MyStoreManager);
                string x = NewUserName.GetDesiredPassword();
                CurrentUser.SetPassword(x);
                x = NewUserName.GetStarredPassword(x);
                Console.WriteLine($"Password changed. {x}");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                EditAccountDetails();
            }
            return;
        }

        public bool CheckEmployee()
        {
            return CurrentUser.GetEmployeeTag();
        }

        public Dictionary<string,Person> GetManagedPeople()
        {
            return MyManagedPeople;
        }

        public List<string> GetManagedPeopleNames()
        {
            return MyPeople;
        }
    }
}