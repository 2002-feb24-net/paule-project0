using System;
using Objects;
using System.Collections.Generic;
using SaveLoad;
// using DataBaseTargets.lib.Model;

namespace Managers
{
    public class StoreManager : IManagerParent
    {
        private Store CurrentStore = null;
        private Dictionary<string,Store> MyManagedStores = new Dictionary<string,Store>();
        private List<string> MyLocations = new List<string>{};
        private static int StoresManaged = 0;
        private OrderManager MyOrderManager;

        public StoreManager()
        {

        }

        public void PullData()
        {
            var MyDataPuller = new Load();
            MyManagedStores = MyDataPuller.PullStoreDictionary();
            MyLocations = MyDataPuller.PullLocationNames();
        }

        public void ReceiveOrderManager(OrderManager InputOrderManager)
        {
            this.MyOrderManager = InputOrderManager;
            foreach (KeyValuePair<string, Store> entry in MyManagedStores)
            {
                entry.Value.ReceiveOrderManager(MyOrderManager);
            }
        }

        public override void SetCurrent(string x)
        {
            CurrentStore = MyManagedStores[x];
        }
        public override int GetTotal()
        {
            return StoresManaged;
        }

        public override bool CheckFor(string x)
        {
            if(MyManagedStores.ContainsKey(x))
            {
                return true;
            }
            return false;
        }

        public override Object Get(string x)
        {
            return MyManagedStores[x];
        }

        public void PrintLocations()
        {
            for (int i = 0;i<MyLocations.Count;i++)
            {
                Console.WriteLine("{0}. {1}",i+1,MyLocations[i]);
            }
        }

        public void Add(Store x)
        {
            MyManagedStores.Add(x.GetName().ToLower(),x);
        }

        public bool CheckLocationNumber(int x)
        {
            if (x<1 || x>MyLocations.Count)
            {
                return false;
            }
            return true;
        }

        public string GetStringLocationByInt(int x)
        {
            string y = MyLocations[x-1];
            return y;
        }

        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX END OF FIRST INITIALIZE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  

        public void Initialize()
        {
            CurrentStore.DisplayGoods();
        }

        public int ShopTopicChoice(int x)
        {
            if (x == 0)
            {
                return 0;
            }
            if (x == 9999)
            {
                int quitint1 = MyOrderManager.PeekOrder();
                if (quitint1 == -1)
                {
                    return 5;
                }
                return 1;
            }
            int quitint2 = CurrentStore.PopulateChosenTopic(x);
            if (quitint2 == -1)
            {
                return 5;
            }
            
            return 1;
        }

        public bool CheckTopicChoice(int x)
        {
            if (x == 9999)
            {
                return true;
            }
            if (x < 0)
            {
                return false;
            }
            return CurrentStore.CheckTopicChoice(x);
        }

        public Dictionary<string,Store> GetManagedStores()
        {
            return MyManagedStores;
        }

        public List<string> GetManagedLocationNames()
        {
            return MyLocations;
        }
    }
}