using System;
using System.Collections.Generic;
using Objects;

namespace Managers
{
    class OrderManager : IManagerParent
    {
        private Dictionary<string,List<Order>> MyManagedOrders = new Dictionary<string,List<Order>>();
        private List<Order> MyCurrentOrder = new List<Order>();
        private static int OrdersManaged = 0;
        private string CurrentUser = null;

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
            CurrentUser = x;
        }

        public void AddToCurrentOrder(Stock x)
        {
            Order NewOrder = new Order(x);
        }
        
    }
}