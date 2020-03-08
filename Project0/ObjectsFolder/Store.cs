using System;
using Managers;

namespace Objects
{
    class Store : ObjectParent
    {
        private string name;
        StockManager MyManager = new StockManager();

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