using System;
using Managers;
using SaveLoad;

namespace Utility
{
    public class MakeNewUser
    {
        private PersonManager MyPersonManager;
        private StoreManager MyStoreManager;
        private InputCollector MyInputCollector;

        public MakeNewUser(PersonManager ThisPersonManager, StoreManager ThisStoreManager)
        {
            MyPersonManager = ThisPersonManager;
            MyStoreManager = ThisStoreManager;
            MyInputCollector = new InputCollector();
        }
        public void CreateNewUser()
        {
            Console.Clear();
            Console.WriteLine("You have been redirected to SIGNUP");
            Console.WriteLine(" ");
            Console.WriteLine("Please fill out the following form.");
            Console.WriteLine(" ");

            string NewUsername = GetDesiredUsername();

            Console.Clear();
            Console.WriteLine("Username: {0}",NewUsername);
            Console.WriteLine(" ");

            string NewLocation = GetDesiredLocation();
            Console.Clear();
            Console.WriteLine("Username: {0}",NewUsername);
            Console.WriteLine("Location: {0}",NewLocation);

            string NewPassword = GetDesiredPassword();
            string NewPasswordStarred = GetStarredPassword(NewPassword);

            MyPersonManager.AddPerson(username:NewUsername,location:NewLocation,password:NewPassword,employee:false);
            Save MySave = new Save();
            MySave.SaveAll();

            Console.WriteLine("Username: {0}",NewUsername);
            Console.WriteLine("Location: {0}",NewLocation);
            Console.WriteLine("Password: {0}",NewPasswordStarred);
            Console.WriteLine(" ");
            Console.WriteLine("Thank you for registering.");
            Console.WriteLine("Press enter to continue.");
            Console.WriteLine(" ");
            Console.ReadLine();
        } 
        public string GetDesiredUsername()
        {
            Console.WriteLine("What is your desired username? (Max 15 characters)");

                string input = MyInputCollector.CreateUsername();
                Console.WriteLine(" ");
                while (MyPersonManager.CheckFor(input))
                {
                    Console.Clear();
                    Console.WriteLine("Username already taken!");
                    input = MyInputCollector.CreateUsername();
                }
            return input;
        }

        public string GetDesiredLocation()
        {
            Console.WriteLine("Which of these locations is closest to you?");
            Console.WriteLine(" ");
            MyStoreManager.PrintLocations();

            int inputNumber = MyInputCollector.GetNumber();
            while (!MyStoreManager.CheckLocationNumber(inputNumber))
            {
                Console.WriteLine("That was not a listed number!");
                inputNumber = MyInputCollector.GetNumber();
            }
            return MyStoreManager.GetStringLocationByInt(inputNumber);
        }

        public string GetDesiredPassword()
        {
            Console.Clear();

            bool passwordSet = false;
            string NewPassword = "";
            while (passwordSet == false)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Please enter your desired password. Note: it will not be starred out.");
                Console.WriteLine("It must have at least 3 caps, no spaces, at least 2 numbers, be over 15 but under 25 characters long, have exactly 1 !, and have the word 'dog' in it.");
                Console.WriteLine(" ");

                NewPassword = MyInputCollector.SetPassword();

                Console.Clear();
                Console.WriteLine(" ");
                Console.WriteLine("Please reenter your desired password.");
                Console.WriteLine(" ");

                string input = Console.ReadLine();
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
            return NewPassword;
        }

        public string GetStarredPassword(string NewPassword)
        {
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
            return NewPasswordStarred;
        }
    }
}