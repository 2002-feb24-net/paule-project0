using System;
using System.Collections.Generic;
using Objects;

namespace Managers
{
    class StockManager : IManagerParent
    {
        private Dictionary<string,Stock> MyManagedStock = new Dictionary<string,Stock>();
        private static int StocksManaged = 0;
        private Stock CurrentStock;
        public override int GetTotal()
        {
            return StocksManaged;
        }

        public override bool CheckFor(string x)
        {
            if(MyManagedStock.ContainsKey(x))
            {
                return true;
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyManagedStock[x];
        }

        public override void SetCurrent(string x)
        {
            CurrentStock = MyManagedStock[x];
        }
    }
}