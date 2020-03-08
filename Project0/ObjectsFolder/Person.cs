using System;

namespace Objects
{
    class Person : ObjectParent
    {
        private string name;
        private string location;
        private string password;

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
    }
}