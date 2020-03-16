using System;

namespace Utility
{
    class PasswordChecker
    {
        public bool CheckIfNewPassIsGood(string input)
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
    }
}