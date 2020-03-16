using System;
using System.Collections;
using Objects;
using System.Collections.Generic;
using Creators;

namespace Managers
{
    class StoreManager : IManagerParent
    {
        private Store CurrentStore = null;
        private StoreCreator MyStoreCreator = new StoreCreator();
        private StockManager MyStockManager = new StockManager();
        private StockCreator MyStockCreator = new StockCreator();
        private Dictionary<string,Store> MyManagedStores = new Dictionary<string,Store>();
        private List<string> MyLocations = new List<string>{"Margaritaville,MA","LaLaLand,CA","UndaDaSea,SC","OldTownRoad,GA","TureReva,XT"};
        private List<string> MyStoreTopics = new List<string>{"Jewelry","Electronics (Non-Phone)","Purses","Wallets","Phones","Household Items","Cars","Gardening Tools","Back"};

        private List<Store> MyStores = new List<Store> {};
        private static int StoresManaged = 0;

        public StoreManager()
        {
            foreach(string value in MyLocations)
            {
                Store NewStore = new Store();
                NewStore.SetName(value.ToLower());

                MyManagedStores.Add(value,NewStore);
            }
            MyStoreCreator.Initialize();
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
            Console.WriteLine("Welcome to the shop!"); 
            Console.WriteLine("All goods here are obtained organically and are hand picked by well-trained human-rights activists.");
            Console.WriteLine("What are you looking for?");
            Console.WriteLine();
            Console.Write("Sections: ");

            for(int i = 0; i<MyStoreTopics.Count;i++)
            {
                Console.WriteLine($"{i}. " + MyStoreTopics[i]);
            }
            Console.WriteLine();
            Console.Write("I'm interested in ");
        }

        public void ShopTopicChoice(int x)
        {
            // "Jewelry","Electronics (Non-Phone)","Purses","Wallets","Phones","Household Items","Cars","Gardening Tools","Back"
            
            switch (x)
            {
                case 1:
                //CurrentStore.PopulateJewelry();
                Console.WriteLine("TODO: Not yet created.");
                break;

                case 2: 
                //PopulateElectronics();
                Console.WriteLine("TODO: Not yet created.");
                break;

                case 3: 
                //PopulatePurses();
                Console.WriteLine("TODO: Not yet created.");
                break;

                case 4: 
                //PopulateWallets();
                Console.WriteLine("TODO: Not yet created.");
                break;

                case 5: 
                //PopulatePhones();
                Console.WriteLine("TODO: Not yet created.");
                break;

                case 6: 
                //PopulateHouseholdItems();
                Console.WriteLine("TODO: Not yet created.");
                break;

                case 7: 
                //PopulateCars();
                Console.WriteLine("TODO: Not yet created.");
                break;

                case 8: 
                //PopulateGardeningTools();
                Console.WriteLine("TODO: Not yet created.");
                break;

                default:
                break;
            }
        }
    }
}