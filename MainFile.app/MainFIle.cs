using System;
using Managers;
using SaveLoad;

namespace MainFile
{
    /// <summary>
    ///  This class is the entry point for the program. The goal was to keep this as simple as possible.
    /// </summary>
    class MainFile
    { 
        /// <summary>
        ///  The main function. Creates the class that acts as the hive mind for the program, and then calls the starting methods from it.
        /// The program will not return to here until it has been fully executed.
        /// Upon return, it asks if the user is done. If yes, the application ends. If no, the application loops.
        /// </summary>
        static void Main(string[] args)
        {
            bool active = true;
            while (active)
            {
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
