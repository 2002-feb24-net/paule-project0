using System;
using Creators;
using Managers;

namespace MainFile
{
    class MainFile
    {
        static void Main(string[] args)
        {

            var MyMainManager = new MainManager();
            MyMainManager.Initialize();
            MyMainManager.MainMenu();



            Console.WriteLine("You have reached the end of the current build.");
        }
    }
}

/*
TODO place orders to store locations for customers
TODO add a new customer (DONE)
TODO search customers by name (DONE)
TODO display details of an order
TODO display all order history of a store location
TODO display all order history of a customer
TODO input validation
TODO (optional: order history can be sorted by earliest, latest, cheapest, most expensive)
TODO (optional: get a suggested order for a customer based on his order history)
TODO (optional: display some statistics based on order history)
*/
