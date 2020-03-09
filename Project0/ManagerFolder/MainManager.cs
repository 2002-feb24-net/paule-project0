using System;
using MainFile;
using Creators;

namespace Managers
{
    class MainManager
    {
        private InputCollector MyInputCollector = new InputCollector();
        private PersonManager MyPersonManager = new PersonManager();
        private StoreManager MyStoreManager = new StoreManager();
        private StoreCreator MyStoreCreator = new StoreCreator();
        private OrderManager MyOrderManager = new OrderManager();
        private OrderCreator MyOrderCreator = new OrderCreator();
        private MenuManager MyMenuManager = new MenuManager();
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
                MakeNewUser();
                return false;
            }
            return true;
        }

        private void MakeNewUser()
        {
            Console.Clear();
            Console.WriteLine("You have been redirected to SIGNUP");
            Console.WriteLine(" ");
            Console.WriteLine("Please fill out the following form.");
            Console.WriteLine(" ");
            Console.WriteLine("What is your desired username? (Max 15 characters)");

            string input = MyInputCollector.CreateUsername();
            Console.WriteLine(" ");
            while (MyPersonManager.CheckFor(input))
            {
                Console.Clear();
                Console.WriteLine("Username already taken!");
                input = MyInputCollector.CreateUsername();
            }

            string NewUsername = input;
            Console.Clear();
            Console.WriteLine("Username: {0}",NewUsername);
            Console.WriteLine(" ");
            Console.WriteLine("Which of these locations is closest to you?");
            Console.WriteLine(" ");
            MyStoreManager.PrintLocations();

            int inputNumber = MyInputCollector.GetNumber();
            while (!MyStoreManager.CheckLocationNumber(inputNumber))
            {
                Console.WriteLine("That was not a listed number!");
                inputNumber = MyInputCollector.GetNumber();
            }
            string NewLocation = MyStoreManager.GetStringLocationByInt(inputNumber);
            Console.Clear();

            bool passwordSet = false;
            string NewPassword = "";
            while (passwordSet == false)
            {
                Console.WriteLine("Username: {0}",NewUsername);
                Console.WriteLine("Location: {0}",NewLocation);
                Console.WriteLine(" ");
                Console.WriteLine("Please enter your desired password. Note: it will not be starred out.");
                Console.WriteLine("It must have at least 3 caps, at least 2 numbers, be over 15 characters long, have exactly 1 !, and have the word 'dog' in it.");
                Console.WriteLine(" ");

                input = MyInputCollector.SetPassword();
                NewPassword = input;

                Console.Clear();
                Console.WriteLine(" ");
                Console.WriteLine("Please reenter your desired password.");
                Console.WriteLine(" ");

                input = Console.ReadLine();
                if (NewPassword != input)
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("Those passwords don't match! Let's try again.");
                    Console.WriteLine(" ");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(" ");
                    Console.WriteLine("Password Set.");
                    Console.WriteLine(" ");
                    passwordSet = true;
                }
            }

            string NewPasswordStarred = "";
            for (int i=0;i<NewPassword.Length;i++)
            {
                if (i==0 || i==NewPassword.Length-1)
                {
                    NewPasswordStarred = NewPasswordStarred + NewPassword[i];
                }
                else
                {
                    NewPasswordStarred = NewPasswordStarred + "*";
                }
            }

            MyPersonManager.AddPerson(username:NewUsername,location:NewLocation,password:NewPassword,employee:false);

            Console.WriteLine("Username: {0}",NewUsername);
            Console.WriteLine("Location: {0}",NewLocation);
            Console.WriteLine("Password: {0}",NewPasswordStarred);
            Console.WriteLine(" ");
            Console.WriteLine("Thank you for registering.");
            Console.WriteLine("Press enter to continue.");
            Console.WriteLine(" ");
            Console.ReadLine();
        } 

        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX END OF INITIALIZE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX  

        public void MainMenu(int choice = 0)
        {
            Console.Clear();
            bool finished = false;
            while (!finished)
            {
                if(choice == 0)
                {
                    //main
                    WelcomeMessageMenu(0);
                    choice = MyInputCollector.ChooseMainMenu();
                }
                else if (choice == 1)
                {
                    //shop
                    WelcomeMessageMenu(1);
                    MyStoreManager.Initialize();
                    MyStoreManager.ShopTopicChoice(MyInputCollector.GetNumber());
                }
                else if (choice == 2)
                {
                    //history
                    WelcomeMessageMenu(2);
                    MyOrderManager.GetUserHistory();
                }
                else if (choice == 3)
                {
                    //account
                    WelcomeMessageMenu(3);
                    MyPersonManager.EditAccountDetails();
                }
                else if (choice == 4)
                {
                    //admin
                    WelcomeMessageMenu(4);
                    if(MyPersonManager.CheckEmployee())
                    {
                        MyStoreManager.EmployeeInitialize();
                    }
                }
                else if (choice == 5)
                {
                    //quit
                    WelcomeMessageMenu(5);
                    finished = true;
                }
            }
        }

        private void WelcomeMessageMenu(int x)
        {
            Console.Clear();
            if (x == 0)
            {
                Console.WriteLine("You have been redirected to: MAIN MENU");
            }
            else if (x == 1)
            {
                Console.WriteLine("You have been redirected to: SHOP");
            }
            else if (x == 2)
            {
                Console.WriteLine("You have been redirected to: HISTORY");
            }
            else if (x == 3)
            {
                Console.WriteLine("You have been redirected to: ACCOUNT");
            }
            else if (x == 4)
            {
                Console.WriteLine("You have been redirected to: ADMIN");
            }
            else if (x == 5)
            {
                Console.WriteLine("Stop by again soon!");
                Console.WriteLine("We get new items all the time.");
            }
        }
    }
}