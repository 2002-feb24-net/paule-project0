using System;
using Utility;
using Creators;

namespace Managers
{
    class MainManager
    {
        private InputCollector MyInputCollector = new InputCollector();
        private PersonManager MyPersonManager = new PersonManager();
        private StoreManager MyStoreManager = new StoreManager();
        private OrderManager MyOrderManager = new OrderManager();
        private OrderCreator MyOrderCreator = new OrderCreator();
        private MenuManager MyMenuManager;

        public MainManager()
        {
            MyMenuManager = new MenuManager(MyInputCollector,MyPersonManager,MyStoreManager,MyOrderManager);
        }
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
            VerifiedUser = false;
            while (!VerifiedUser)
            {
                input = MyInputCollector.GetPassword();
                VerifiedUser = MyPersonManager.CheckCurrentPassword(input);
            }
        }

        private void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Hello and welcome to Paul's Totally (not) Sketchy Used Goods!");
            Console.WriteLine("Please log in! If you do not have an account please type: SIGNUP");
        }

        private bool Verify(bool x)
        {
            if (!x)
            {
                var myNewUser = new MakeNewUser();
                myNewUser.CreateNewUser(MyInputCollector : this.MyInputCollector, MyPersonManager: this.MyPersonManager, MyStoreManager: this.MyStoreManager);
                return false;
            }
            return true;
        }

        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX END OF INITIALIZE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  

        public void MainMenu(int choice = 0)
        {
            MyMenuManager.InitializeMainMenu();
            MyPersonManager.Serialize();
        }
    }
}