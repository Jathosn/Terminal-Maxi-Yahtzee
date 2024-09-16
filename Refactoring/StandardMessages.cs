using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    internal class StandardMessages
    {
        public static void WelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Welcome to Terminal Maxi Yahtzee.\n");
            Console.ResetColor();        }
        public static void TurnEnded()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nTurn ended: Enter category\n");
            Console.ResetColor();
        }
        public static void InvalidCategory()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid category or already scored. Please try again.");
            Console.ResetColor();
        }
        public static void DiceToKeep()
        {
            Console.WriteLine("Write the values (1-6) you wish to keep. Press ENTER to continue");
        }
        public static void RerollDice()
        {
            Console.WriteLine("Rerolling all dice...");
        }
        public static void InvalidDiceInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter numbers between 1 and 6.");
            Console.ResetColor();
        }
        public static void InvalidScoreboardInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Input value does not exist. Please try again.");
            Console.ResetColor();
        }
        public static void InvalidPlayerCountEntry()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter a positive integer greater than 0.");
            Console.ResetColor();
        }
        public static void InvalidPlayerNameEntry()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Name cannot be empty");
            Console.ResetColor();
        }
        public static void NavbarPrompt()
        {
            Console.WriteLine($"Press 'ENTER' to throw\nPress 'S' to view scoreboard \nPress 'E' to end turn \nPress 'H' to view shorthand notations");
        }
        public static void OutOfThrows()
        {
            Console.WriteLine("No throws remaining.");
        }
        public static void TurnEndResult() {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Result:");
            Console.ResetColor();
        }
        public static void GameEndScore()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Game Over. Final Score: \n");
            Console.ResetColor();
        }
    }
}
