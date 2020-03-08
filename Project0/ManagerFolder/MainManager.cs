using System;
using MainFile;

namespace Managers
{
    class MainManager
    {
        InputCollector MyInputCollector = new InputCollector();
        PersonManager MyPersonManager = new PersonManager();
        StoreManager MyStoreManager = new StoreManager();
        OrderManager MyOrderManager = new OrderManager();
        public void Initialize()
        {
            WelcomeMessage();
            string input;
            bool VerifiedUser = false;
            while (!VerifiedUser)
            {
                input = MyInputCollector.GetUserName();
                VerifiedUser = Verify(MyPersonManager.CheckFor(input));
            }
            Console.WriteLine("You have reached the end of the current build.");
        }

        private void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("Hello and welcome to Paul's (not) Sketchy Used Goods Store!");
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

            MyPersonManager.Add(username:NewUsername,location:NewLocation,password:NewPassword);

            Console.WriteLine("Username: {0}",NewUsername);
            Console.WriteLine("Location: {0}",NewLocation);
            Console.WriteLine("Password: {0}",NewPasswordStarred);
            Console.WriteLine(" ");
            Console.WriteLine("Thank you for registering.");
            Console.WriteLine("Press enter to continue.");
            Console.WriteLine(" ");
            Console.ReadLine();
        }   
    }
}