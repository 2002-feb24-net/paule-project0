using System;

namespace Objects
{
    class Stock : ObjectParent
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