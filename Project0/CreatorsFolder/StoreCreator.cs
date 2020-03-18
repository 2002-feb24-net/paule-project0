using System;
using Objects;
using System.Collections.Generic;
using Utility;
using SerDSer;

namespace Creators
{
    class StoreCreator
    {
        private Dictionary<string,Store> MyStores = new Dictionary<string,Store>{};
        private Deserializer MyDeserializer = new Deserializer();
        private string myPath = "../../../MargaritavilleStock.json";
        private List<string> MyStoreLocations = new List<string>{"Margaritaville,MA","LaLaLand,CA","UndaDaSea,SC","OldTownRoad,GA","TureReva,XT"};
        public void Initialize()
        {
            MyStores = MyDeserializer.DeserializeStore(myPath);
            // StoreStocker MyStoreStocker = new StoreStocker();
            // for (int i = 0; i<MyStoreLocations.Count;i++)
            // {
            //     Store NewStore = new Store();
            //     NewStore.SetName(MyStoreLocations[i]);
            //     NewStore.SetNameAsInt(i);
            //     MyStoreStocker.StockStore(NewStore);
            //     MyStores.Add(NewStore);
            // }
        }

        public void CreateStore(string location,Dictionary<string,List<Stock>> x)
        {
            //"Jewelry","Electronics (Non-Phone)","Purses","Wallets","Phones","Household Items","Cars","Gardening Tools","Back"
            Store y = new Store();
            y.SetName(location);
            //Dictionary<string,List<Stock>>
            y.SetMyDictionary(x);
        }
    }
}