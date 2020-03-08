using System;
using System.Collections;
using Objects;
using System.Collections.Generic;

namespace Managers
{
    class StoreManager : ManagerParent
    {
        Hashtable MyHashTable = new Hashtable();
        List<string> MyLocations = new List<string>{"Margaritaville,MA","LaLaLand,CA","UndaDaSea,SC","OldTownRoad,GA","TureReva,XT"};
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
    }
}