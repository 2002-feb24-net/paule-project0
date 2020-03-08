using System;

namespace Objects
{
    class Order : ObjectParent
    {
        private string name;

        public void SetName(string x)
        {
            this.name = x;
        }

        public string GetName()
        {
            return this.name;
        }
    }
}