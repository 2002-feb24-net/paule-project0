using System;
using Objects;
using System.Collections.Generic;
using SerDSer;

namespace Creators
{
    class PersonCreator
    {
        private List<Person> MyPeople = new List<Person>{};
        private List<string> MyStartingNames = new List<string>{"revature"};
        private List<string> MyStartingLocations = new List<string>{"TureReva,XT"};
        private List<string> MyStartingPasswords = new List<string>{"admin"};
        private List<bool> MyStartingEmployees = new List<bool>{true};
        
        public PersonCreator()
        {
            Initialize();
        }
        public Person CreatePerson(string username,string location,string password,bool employee)
        {
            Person NewPerson = new Person();
            NewPerson.SetLocation(location);
            NewPerson.SetName(username);
            NewPerson.SetPassword(password);
            NewPerson.SetEmployeeTag(employee);
            return NewPerson;
        }

        private void Initialize()
        {
            if (MyStartingNames.Count != MyStartingLocations.Count || MyStartingLocations.Count != MyStartingPasswords.Count || MyStartingPasswords.Count != MyStartingEmployees.Count)
            {
                Console.WriteLine("Critical error: Starting inputs for users are not equal. Initialization cancelled.");
            }
            else
            {   for (int i = 0;i<MyStartingLocations.Count;i++)
                {
                    MyPeople.Add(CreatePerson(username:MyStartingNames[i],location:MyStartingLocations[i],password:MyStartingPasswords[i],employee:MyStartingEmployees[i]));
                }
            }
        }

        public List<Person> GetInitialUsers()
        {
            return MyPeople;
        }
    }
}