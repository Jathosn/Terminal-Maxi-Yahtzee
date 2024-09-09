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
            List<Player> players = new List<Player>();
            StandardMessages.WelcomeMessage();
            PlayerData.PlayerCount();
            PlayerData.PlayerName();

            Console.WriteLine("Players added.");

            StandardMessages.GameStarting();

            foreach (var player in PlayerData.players)  // Use PlayerData.players if players are being stored there
            {
                // Check if the player's scoreboard is complete
                bool isComplete = PlayerData.IsScoreboardComplete(player);

                // Print the result to the console
                Console.WriteLine($"{player.Name}'s scoreboard is complete: {isComplete}");

                if (!isComplete)
                {
                    PlayerTurnHandler turnHandler = new PlayerTurnHandler(player);
                    Console.WriteLine($"Handler escaped");
                }
            }

            Console.ReadKey();
        }
    }
}
