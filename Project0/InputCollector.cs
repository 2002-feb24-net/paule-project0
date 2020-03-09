using System;
using System.Collections.Generic;

namespace MainFile
{
    class InputCollector
    {
        /*
        Need the following:
        store location - we see that you have never been here, please choose your location
        customer info - please log in
            returns log in info to MainManager
                main manager checks if customer is in the database
        if a returning customer,
        send to options class.
        option class can create another inputcollector if needed (meaning we cant have constructor methods if we do that)
        */

        List<string> BadNameList = new List<string> {"This is going to be a long throwaway string that censors the words for anyone looking a this code in visual studio code. Yes this is a very very simple censor system but you get the idea.                                      ","fuck","ass","bitch","penis","vagina","nigger","negro","bastard","cunt","faggot","fag","retard","pussy","testicle","cock","whore","blowjob","idiot","slut","chink","dildo","masturbate","boob","boobies","booby","boobys","@$$","cuck","autism","autistic"};
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
            Console.Write("Desired number: ");
            string input = Console.ReadLine();
            Console.WriteLine(" ");
            return int.Parse(input);
        }

        public string SetPassword()
        {
            //It must have at least 3 caps, no spaces, at least 2 numbers, be over 15 but under 25 characters long, have exactly 1 !, and have the word 'dog' in it.
            bool GoodPass = false;
            string input = "";
            while (GoodPass == false)
            {
                Console.Write("Desired password: ");
                input = Console.ReadLine();
                GoodPass = CheckIfPassIsGood(input);
            }
            return input;

        }

        private bool CheckIfPassIsGood(string input)
        {
            if (input.Length <= 15 || input.Length >= 25)
            {
                Console.Clear();
                Console.WriteLine("That password is the wrong length!");
                Console.WriteLine("It must be over 15 and under 25 characters!");
                return false;
            }
            char[] inputChar = input.ToCharArray();
            int counter = 0;
            for (int i = 0;i<inputChar.Length;i++)
            {
                if (char.IsUpper(inputChar[i]))
                {
                    counter++;
                }
            }
            if (counter < 3)
            {
                Console.Clear();
                Console.WriteLine("You need a least 3 capital letters!");
                return false;
            }
            if (input.Contains(" "))
            {
                Console.Clear();
                Console.WriteLine("You can't have any spaces!");
                return false;
            }
            counter = 0;
            for (int i = 0;i<inputChar.Length;i++)
            {
                if (char.IsDigit(inputChar[i]))
                {
                    counter++;
                }
            }
            if (counter < 2)
            {
                Console.Clear();
                Console.WriteLine("You need at least two numbers!");
                return false;
            }
            
            counter = 0;
            for (int i = 0;i<inputChar.Length;i++)
            {
                if (inputChar[i] == '!')
                {
                    counter++;
                }
            }
            if (counter != 1)
            {
                Console.Clear();
                Console.WriteLine("You must have exactly one '!'");
                return false;
            }

            if(!input.ToLower().Contains("dog"))
            {
                Console.Clear();
                Console.WriteLine("You must include the word 'dog' !");
                return false;
            }

            return true;
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

        public int ChooseMainMenu()
        {
            Console.WriteLine("Please choose one of the following options.");
            string[] options = new string[] {"SHOP","HISTORY","ACCOUNT","ADMIN","QUIT"};
            for (int i = 0 ; i<options.Length;i++)
            {
                Console.Write(i+1 + " ");
                Console.WriteLine(options[i]);
            }
            int input = 0;
            while (input < 1)
            {
                input = GetNumber();
                if (input > options.Length || input <= 0)
                {
                    Console.WriteLine("That number is not on the list!");
                    input = 0;
                }
            }
            return input;
        }
    }
}