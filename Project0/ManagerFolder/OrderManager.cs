using System;
using System.Collections;

namespace Managers
{
    class OrderManager : ManagerParent
    {
        Hashtable MyHashTable = new Hashtable();
        static int OrdersManaged = 0;
        public override int GetTotal()
        {
            return OrdersManaged;
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