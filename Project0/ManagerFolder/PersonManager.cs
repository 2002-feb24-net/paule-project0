using System;
using System.Collections;
using System.Collections.Generic;
using Objects;

namespace Managers
{
    class PersonManager : ManagerParent
    {
        Hashtable MyHashTable = new Hashtable();
        List<string> MyPeople = new List<string>{"bob","larry","susy","leonardo","revature"};
        static int PersonsManaged = 0;

        public PersonManager()
        {
            foreach(string value in MyPeople)
            {
                Person NewPerson = new Person();
                NewPerson.SetName(value);

                MyHashTable.Add(value,NewPerson);
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
            
            if(MyHashTable.Contains(x.ToLower()))
            {
                return true;
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyHashTable[x];
        }

        public void Add(Person x)
        {
            MyPeople.Add(x.GetName().ToLower());
            MyHashTable.Add(x.GetName().ToLower(),x);
        }

        public void Add(string username, string location, string password)
        {
            Person NewPerson = new Person();
            NewPerson.SetLocation(location);
            NewPerson.SetName(username);
            NewPerson.SetPassword(password);
            MyPeople.Add(username.ToLower());
            MyHashTable.Add(username.ToLower(),NewPerson);
        }
    }
}