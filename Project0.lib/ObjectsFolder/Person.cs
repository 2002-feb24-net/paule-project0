using System;

namespace Objects
{
    public class Person : ObjectParent
    {
        public string name {get; set;}
        public string location {get; set;}
        public string password {get; set;}
        public bool EmployeeTag {get; set;}

        public void SetName(string x)
        {
            this.name = x;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetLocation(string x)
        {
            this.location = x;
        }

        public string GetLocation()
        {
            return this.location;
        }

        public void SetPassword(string x)
        {
            this.password = x;
        }

        public string GetPassword()
        {
            return this.password;
        }

        public void SetEmployeeTag(bool x)
        {
            this.EmployeeTag = x;
        }

        public bool GetEmployeeTag()
        {
            return this.EmployeeTag;
        }
    }
}