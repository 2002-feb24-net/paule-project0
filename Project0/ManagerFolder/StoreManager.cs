using System;
using Objects;
using System.Collections.Generic;
using Creators;
using SerDSer;

namespace Managers
{
    class StoreManager : IManagerParent
    {
        private Store CurrentStore = null;
        private StoreCreator MyStoreCreator = new StoreCreator();
        private Dictionary<string,Store> MyManagedStores = new Dictionary<string,Store>();
        private List<string> MyLocations = new List<string>{};
        private List<string> MyStoreTopics = new List<string>{"Jewelry","Electronics (Non-Phone)","Purses","Wallets","Phones","Household Items","Cars","Gardening Tools","Back"};
        private static int StoresManaged = 0;
        private OrderManager MyOrderManager;

        private string myPath = "../.json/MasterLocationList.json";

        public StoreManager()
        {
            var MyDeserializer = new Deserializer();
            MyManagedStores = MyDeserializer.DeserializeStore(myPath);
            foreach (KeyValuePair<string, Store> entry in MyManagedStores)
            {
                entry.Value.Initialize();
                MyLocations.Add(entry.Key);
            }
            MyStoreCreator.Initialize();
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
                // MyOrderManager.PeekOrder();
                Console.WriteLine("TODO: Not yet created.");
                return 1;
            }
            CurrentStore.PopulateChosenTopic(x);
            return 1;
        }

        public void Serialize()
        {
            var MySerializer = new Serializer();
            MySerializer.Serialize(myPath,MyManagedStores);
            CurrentStore.Serialize();
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
    }
}