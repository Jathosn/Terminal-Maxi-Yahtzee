using System;
namespace Refactoring
{
    internal class StandardMessages
    {
        public static void WelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Welcome to Terminal Maxi Yahtzee.\n");
            Console.ResetColor();        
        }
        public static void NavbarPrompt()
        {
            Console.WriteLine($"Press 'ENTER' to throw\nPress 'S' to view scoreboard \nPress 'E' to end turn \nPress 'H' to view shorthand notations");
        }
    }
}
