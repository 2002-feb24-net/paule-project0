using System;
using Managers;
using SaveLoad;

namespace MainFile
{
    class MainFile
    {
        static void Main(string[] args)
        {
            bool active = true;
            while (active)
            {
                Load x = new Load();
                var MyMainManager = new MainManager();
                MyMainManager.Initialize();
                MyMainManager.MainMenu();
                Console.WriteLine("You have reached the end of the current build.");
                Console.WriteLine("Changes have been saved.");
                Console.WriteLine("Enter B to exit the application. Enter anything else to restart.");
                string y = Console.ReadLine();
                if (y.ToUpper() == "B")
                {
                    active = false;
                }
            }
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
TODO don't use public fields
TODO define and use at least one interface
TODO exception handling
TODO (optional: asynchronous file I/O)
TODO persistent data (read from file); no prices, customers, order history, etc. hardcoded in C#
TODO serialize data to disk
TODO deserialize data from disk on startup
VV core / domain / business logic VV
TODO class library
TODO contains all business logic
TODO contains domain classes (customer, order, store, product, etc.)
TODO documentation with <summary> XML comments on all public types and members (optional: <params> and <return>)
TODO (recommended: has no dependency on UI, data access, or any input/output considerations)
VV customer VV
TODO has first name, last name, etc.
TODO (optional: has a default store location to order from)
VV order VV
TODO has a store location
TODO has a customer
TODO has an order time (when the order was placed)
TODO can contain multiple kinds of product in the same order
TODO rejects orders with unreasonably high product quantities
TODO (optional: some additional business rules, like special deals)
VV location VV
TODO has an inventory
TODO inventory decreases when orders are accepted
TODO rejects orders that cannot be fulfilled with remaining inventory
TODO (optional: for at least one product, more than one inventory item decrements when ordering that product)
TODO user interface
VV interactive console application VV
TODO has only display- and input-related code
TODO low-priority component, will be replaced when we move to project 1
TODO data access (recommended)
VV class library VV
TODO recommended separate project for serialization and deserialization code
TODO contains data access logic but no business logic
VV test VV
TODO at least 10 test methods
TODO use TDD for some of the application
TODO focus on unit testing business logic; testing the console app is very low priority
*/
