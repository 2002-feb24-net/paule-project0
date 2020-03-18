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
        }

        public Store CreateStore(string location,string path)
        {
            Store y = new Store();
            y.SetName(location);
            y.SetPath(path);
            return(y);
        }
    }
}