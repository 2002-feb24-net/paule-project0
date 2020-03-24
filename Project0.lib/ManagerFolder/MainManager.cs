using System;
using Utility;
using Objects;
using SaveLoad;

namespace Managers
{
    public class MainManager
    {
        private InputCollector MyInputCollector = new InputCollector();
        private PersonManager MyPersonManager = new PersonManager();
        private StoreManager MyStoreManager = new StoreManager();
        private OrderManager MyOrderManager;
        private MenuManager MyMenuManager;

        public MainManager()
        {
            MyOrderManager = new OrderManager(this);
            Load y = new Load();
            y.LoadAllInfo(MyPersonManager,MyStoreManager,MyOrderManager);
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
            Person CurrentUser = MyPersonManager.GetCurrentUser();
            SetToCurrentGlobal(CurrentUser);
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
                var myNewUser = new MakeNewUser(ThisPersonManager: this.MyPersonManager, ThisStoreManager: this.MyStoreManager);
                myNewUser.CreateNewUser();
                return false;
            }
            return true;
        }

        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX END OF INITIALIZE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  

        public void MainMenu()
        {
            MyMenuManager.InitializeMainMenu();
            // only reaches here after quitting
            Save z = new Save();
            z.SaveAll(this,MyPersonManager,MyStoreManager,MyOrderManager);
        }

        private void SetToCurrentGlobal(Person x)
        {
            MyPersonManager.SetCurrentUser(x);
            MyStoreManager.SetCurrent(x.GetLocation());
            MyOrderManager.SetCurrent(x.GetName());
            MyStoreManager.ReceiveOrderManager(MyOrderManager);
            MyPersonManager.ReceiveStoreManager(MyStoreManager);
        }

        public void TossBackItem(Stock x)
        {
            MyStoreManager.GetManagedStores()[MyPersonManager.GetCurrentUser().GetLocation()].AddStock(x);
        }
    }
}