using System;
using System.Collections;

namespace Managers
{
    class StockManager : ManagerParent
    {
        Hashtable MyHashTable = new Hashtable();
        static int StocksManaged = 0;
        public override int GetTotal()
        {
            return StocksManaged;
        }

        public override bool CheckFor(string x)
        {
            if(MyHashTable.Contains(x))
            {
                return true;
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyHashTable[x];
        }
    }
}