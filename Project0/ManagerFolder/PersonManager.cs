using System;
using System.Collections.Generic;
using Objects;
using Creators;
using SerDSer;

namespace Managers
{
    class PersonManager : IManagerParent
    {
        private Dictionary<string,Person> MyManagedPeople = new Dictionary<string,Person>();
        private List<string> MyPeople = new List<string>{};
        private PersonCreator MyPersonCreator = new PersonCreator();
        private static int PersonsManaged = 0;
        private Person CurrentUser = null;
        private string myPath = "../.json/Users.json";

        public PersonManager()
        {
            var MyDeserializer = new Deserializer();
            MyManagedPeople = MyDeserializer.DeserializePerson(myPath);
            foreach(KeyValuePair<string, Person> entry in MyManagedPeople)
            {
                MyPeople.Add(entry.Key);
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
            return MyManagedPeople[x];
        }

        public void AddPerson(string username,string location,string password,bool employee)
        {
            Person x = MyPersonCreator.CreatePerson(username,location,password,employee);
            MyPeople.Add(x.GetName().ToLower());
            MyManagedPeople.Add(x.GetName().ToLower(),x);
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
            x = x.ToLower();
            if(MyManagedPeople.ContainsKey(x))
            {
                return (Person) MyManagedPeople[x];
            }
            Console.WriteLine("Critical error: No user selected.");
            return null;
        }

        public override void SetCurrent(string x)
        {
            CurrentUser = MyManagedPeople[x];
        }

        public void Serialize()
        {
            var MySerializer = new Serializer();
            MySerializer.Serialize(myPath,MyManagedPeople);
        }
    }
}