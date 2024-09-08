using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting the game setup...");

            List<Player> players = new List<Player>();
            StandardMessages.WelcomeMessage();
            PlayerData.PlayerCount();

            Console.WriteLine("Number of players set.");

            PlayerData.PlayerName();

            Console.WriteLine("Players added.");

            StandardMessages.GameStarting();

            GameFlow gameflow = new GameFlow(players);
            gameflow.StartGame();

            Console.WriteLine("Game started.");
            Console.ReadKey();  // Keep the console open
        }
    }
}
