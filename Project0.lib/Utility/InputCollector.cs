using System;
using System.Collections.Generic;

namespace Utility
{
    public class InputCollector
    {
        private List<string> BadNameList = new List<string> {"This is going to be a long throwaway string that censors the words for anyone looking a this code in visual studio code. Yes this is a very very simple censor system but you get the idea.                                      ","fuck","ass","bitch","penis","vagina","nigger","negro","bastard","cunt","faggot","fag","retard","pussy","testicle","cock","whore","blowjob","idiot","slut","chink","dildo","masturbate","boob","boobies","booby","boobys","@$$","cuck","autism","autistic"};
        public string GetUserName()
        {
            Console.Write("Username: ");
            string input = Console.ReadLine();
            return input;
        }

        private bool CheckForBadName(string x)
        {
            foreach (string value in BadNameList)
            {
                if ((x.ToLower()).Contains(value))
                {
                    return true;
                }
            }
            return false;
        }

        public string CreateUsername()
        {
            Console.Write("Desired Username: ");
            string input = Console.ReadLine();
            while (input.Length > 15)
            {
                Console.Clear();
                Console.WriteLine("Your entered username of {0} is too long! (Max 15 characters)",input);
                Console.Write("Desired Username: ");
                input = Console.ReadLine();
            }
            while (CheckForBadName(input))
            {
                Console.Clear();
                Console.WriteLine("Your entered username has a blacklisted phrase!");
                return (CreateUsername());
            }
            return input;
        }

        public int GetNumber()
        {
            bool GoodNumber = false;
            while (!GoodNumber)
            {
                Console.Write("Desired Number: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "b")
                {
                    return 0;
                }
                if (input.ToLower() == "o")
                {
                    return 9999;
                }
                Console.WriteLine(" ");
                try
                {
                    int output = int.Parse(input);
                    return output;
                }
                catch (Exception)
                {
                    Console.WriteLine("You must input a number!");
                }
            }
            Console.WriteLine("Error in number collection. This message should never show.");
            return -1;
        }

        public string SetPassword()
        {
            //It must have at least 3 caps, no spaces, at least 2 numbers, be over 15 but under 25 characters long, have exactly 1 !, and have the word 'dog' in it.
            var passCheck = new PasswordChecker();
            bool GoodPass = false;
            string input = "";
            while (GoodPass == false)
            {
                Console.Write("Desired password: ");
                input = Console.ReadLine();
                GoodPass = passCheck.CheckIfNewPassIsGood(input);
            }
            return input;
        }

        public string GetPassword()
        {
            Console.WriteLine("Please enter your password.");
            Console.Write("Password: ");
            return Console.ReadLine();
        }

        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX END OF INITIALIZE XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //! XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX 

        public int ChooseMenu(int x)
        {
            int options;
            var MyCurrentMenu = new MenuOptionsDisplay(x, out options);
            int input = 0;
            while (input < 1)
            {
                input = GetNumber();
                if (input > options || input <= 0)
                {
                    Console.WriteLine("That number is not on the list!");
                    input = 0;
                }
            }
            return input;
        }

        public string InputCity()
        {
            Console.Write("City: ");
            string x = Console.ReadLine();
            x = ConvertToPureString(x);
            return x;
        }

        public string ConvertToPureString(string x)
        {
            char[] arr = x.ToCharArray();
            arr = Array.FindAll<char>(arr, (c => (char.IsLetterOrDigit(c))));
            x = new string(arr);
            return x;
        }
    }
}