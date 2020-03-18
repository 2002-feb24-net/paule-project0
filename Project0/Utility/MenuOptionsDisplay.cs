using System;

namespace Utility
{
    class MenuOptionsDisplay
    {
        private int chosenMenu;
        public MenuOptionsDisplay(int x, out int y)
        {
            this.chosenMenu = x;
            y = SortMenuMethod(x);

        }

        private int SortMenuMethod(int x)
        {
            switch (x)
            {
                case 0:
                DisplayMainMenu();
                return 5;
            }
            return -1;
        }

        private void DisplayMainMenu()
        {
            Console.WriteLine("Please choose one of the following options.");
            string[] options = new string[] {"SHOP","HISTORY","ACCOUNT","ADMIN","QUIT"};
            for (int i = 0 ; i<options.Length;i++)
            {
                Console.Write(i+1 + ". ");
                Console.WriteLine(options[i]);
            }
        }
    }
}