using System;
using System.Collections;
using Objects;
using System.Collections.Generic;
using Creators;

namespace Managers
{
    class StoreManager : ManagerParent
    {
        StockManager MyStockManager = new StockManager();
        StockCreator MyStockCreator = new StockCreator();
        Hashtable MyHashTable = new Hashtable();
        List<string> MyLocations = new List<string>{"Margaritaville,MA","LaLaLand,CA","UndaDaSea,SC","OldTownRoad,GA","TureReva,XT"};
        List<string> MyStoreTopics = new List<string>{"Jewelry","Electronics (Non-Phone)","Purses","Wallets","Phones","Household Items","Cars","Gardening Tools","Back"};
        static int StoresManaged = 0;

        public StoreManager()
        {
            foreach(string value in MyLocations)
            {
                Store NewStore = new Store();
                NewStore.SetName(value.ToLower());

                MyHashTable.Add(value,NewStore);
            }
        }
        public override int GetTotal()
        {
            return StoresManaged;
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

        public void PrintLocations()
        {
            for (int i = 0;i<MyLocations.Count;i++)
            {
                Console.WriteLine("{0}. {1}",i+1,MyLocations[i]);
            }
        }

        public void Add(Store x)
        {
            MyHashTable.Add(x.GetName().ToLower(),x);
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
            Console.WriteLine("Welcome to the shop!"); 
            Console.WriteLine("All goods here are obtained organically and are hand picked by well-trained human-rights activists.");
            Console.WriteLine("What are you looking for?");
            Console.WriteLine();
            Console.Write("Sections: ");

            foreach (string value in MyStoreTopics)
            {
                Console.Write(value + " ");
            }
            Console.WriteLine();
            Console.Write("I'm interested in: ");
        }
    }
}