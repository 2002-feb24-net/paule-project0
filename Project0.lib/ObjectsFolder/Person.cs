using System;

namespace Objects
{
    public class Person : ObjectParent
    {
        private string name {get; set;}
        private string location {get; set;}
        private string password {get; set;}
        private bool EmployeeTag {get; set;}

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