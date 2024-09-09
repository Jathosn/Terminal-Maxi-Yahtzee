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
            bool allPlayersComplete = false;
            while (!allPlayersComplete)
            {
                foreach (var player in PlayerData.players)
                {
                    bool isComplete = PlayerData.IsScoreboardComplete(player);

                    Console.WriteLine($"{player.Name}'s scoreboard is complete: {isComplete}");

                    if (!isComplete)
                    {
                        PlayerTurnHandler turnHandler = new PlayerTurnHandler(player);
                        Console.WriteLine($"Handler escaped");
                    }
                }
                allPlayersComplete = PlayerData.players.All(p => PlayerData.IsScoreboardComplete(p));
            }
               

            Console.ReadKey();
        }
    }
}
