using System;
using System.Collections.Generic;
using Objects;

namespace Managers
{
    class OrderManager : IManagerParent
    {
        private Dictionary<string,Order> MyManagedOrders = new Dictionary<string,Order>();
        static int OrdersManaged = 0;
        private Order CurrentOrder = null;
        public override int GetTotal()
        {
            return OrdersManaged;
        }

        public override bool CheckFor(string x)
        {
            if(MyManagedOrders.ContainsKey(x))
            {
                return true;
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyManagedOrders[x];
        }

        public override void SetCurrent(string x)
        {
            CurrentOrder = MyManagedOrders[x];
        }
    }
}