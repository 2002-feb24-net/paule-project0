using System;
using System.Collections;
using System.Collections.Generic;
using Objects;
using Creators;

namespace Managers
{
    class PersonManager : ManagerParent
    {
        private Hashtable MyHashTable = new Hashtable();
        private List<string> MyPeople = new List<string>{};
        private PersonCreator MyPersonCreator = new PersonCreator();
        private static int PersonsManaged = 0;
        private Person CurrentUser = null;

        public PersonManager()
        {
            foreach (Person value in MyPersonCreator.GetInitialUsers())
            {
                MyHashTable.Add(value.GetName(),value);
                MyPeople.Add(value.GetName());
            }
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
            
            if(MyPeople.Contains(x.ToLower()))
            {
                return true;
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyHashTable[x];
        }

        public void AddPerson(string username,string location,string password,bool employee)
        {
            Person x = MyPersonCreator.CreatePerson(username,location,password,employee);
            MyPeople.Add(x.GetName().ToLower());
            MyHashTable.Add(x.GetName().ToLower(),x);
        }

        public void SetCurrentUser(Person x)
        {
            CurrentUser = x;
        }

        public bool CheckCurrentPassword(string x)
        {
            Console.WriteLine("Checking {0} vs {1}",x,CurrentUser.GetPassword());
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
            if(MyHashTable.Contains(x.ToLower()))
            {
                return (Person) MyHashTable[x];
            }
            Console.WriteLine("Critical error: No current user selected.");
            return null;
        }
    }
}