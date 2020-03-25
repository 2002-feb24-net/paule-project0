using System;
using Utility;
using Objects;
using SaveLoad;

namespace Managers
{
    /// <summary>
    ///  This class is main logic handler. The overmind that links all of the managers together. Any branch of logic can be reached from here. It is created by the main method
    /// when the application starts, and returns here after quitting. Some classes use this as a hub to gain access to the other managers.
    /// </summary>
    public class MainManager
    {
        private InputCollector MyInputCollector = new InputCollector();
        private PersonManager MyPersonManager = new PersonManager();
        private StoreManager MyStoreManager = new StoreManager();
        private OrderManager MyOrderManager;
        private MenuManager MyMenuManager;

        /// <summary>
        ///  This constructor imports all the needed data from the database by creating a load object. It also creates a save and menumanager class, and this is only done to
        /// pass in the managers that are going to be used for the program.
        /// </summary>
        public MainManager()
        {
            MyOrderManager = new OrderManager(this);
            Load y = new Load();
            y.LoadAllInfo(MyPersonManager,MyStoreManager,MyOrderManager);
            Save z = new Save();
            z.LoadAll(this,MyPersonManager,MyStoreManager,MyOrderManager);
            MyMenuManager = new MenuManager(MyInputCollector,MyPersonManager,MyStoreManager,MyOrderManager);
        }
        /// <summary>
        ///  This method calls a welcome method, and then starts a chain off logic that represents logging in. Upon successful username lookup, it passes the info
        /// to the person manager to set the current user of itself, and then asks it what the current user's password should be. If it matches, calls a method to set the
        /// current user for all managers. This logic can also send you to a user creation logic line if it does not find the username that you input.
        /// Saves after logging in, to ensure misc info related to creating a new user is in the database.
        /// </summary>
        public void Initialize()
        {
            WelcomeMessage();
            string input;
            bool VerifiedUser = false;
            while (!VerifiedUser)
            {
                input = MyInputCollector.GetUserName();
                MyPersonManager.SetCurrentUser(MyPersonManager.GetUser(input));
                VerifiedUser = Verify(MyPersonManager.CheckFor(input));
            }
            Person CurrentUser = MyPersonManager.GetCurrentUser();
            SetToCurrentGlobal(CurrentUser);
            VerifiedUser = false;
            while (!VerifiedUser)
            {
                input = MyInputCollector.GetPassword();
                VerifiedUser = MyPersonManager.CheckCurrentPassword(input);
            }
            Save MySave = new Save();
            MySave.SaveAll();
        }
        /// <summary>
        ///  This method is a simple message to the user.
        /// </summary>
        private void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Hello and welcome to Paul's (Totally not Sketchy) Used Goods!");
            Console.WriteLine("Please log in! If you do not have an account please type: SIGNUP");
        }

        /// <summary>
        ///  This method handles the results of the username check. The results of the check are passed in as a parameter, and if the check fails, this method directs you to a 
        /// user creation logic line. Also passes in the managers to the user creation object.
        /// </summary>
        private bool Verify(bool x)
        {
            if (!x)
            {
                var myNewUser = new MakeNewUser(ThisPersonManager: this.MyPersonManager, ThisStoreManager: this.MyStoreManager);
                myNewUser.CreateNewUser();
                return false;
            }
            return true;
        }

        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX END OF INITIALIZE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  

        /// <summary>
        ///  This method is the access point to the main menu. If the program reaches here, the current user and location has been selected and set.
        /// The application does not return here until after quitting the main menu. Saves all data upon exiting, and returns back to the main method.
        /// </summary>
        public void MainMenu()
        {
            MyMenuManager.InitializeMainMenu();
            // only reaches here after quitting
            Save z = new Save();
            z.SaveAll();
        }

        /// <summary>
        ///  This method sets the current user for all managers, and general consistency maintenance like initializing the other managers for each other.
        /// </summary>
        private void SetToCurrentGlobal(Person x)
        {
            MyPersonManager.SetCurrentUser(x);
            MyStoreManager.SetCurrent(x.GetLocation());
            MyOrderManager.SetCurrent(x.GetName());
            MyStoreManager.ReceiveOrderManager(MyOrderManager);
            MyPersonManager.ReceiveStoreManager(MyStoreManager);
        }

        /// <summary>
        ///  This method just passes along an object that is no longer wanted buy the buyer. Passes it back to the store manager for replacement in the store.
        /// </summary>
        public void TossBackItem(Stock x)
        {
            MyStoreManager.GetManagedStores()[MyPersonManager.GetCurrentUser().GetLocation()].AddStock(x);
        }
    }
}