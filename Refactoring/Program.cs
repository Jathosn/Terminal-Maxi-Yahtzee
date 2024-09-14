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
            ScoreCalculator scoreCalculator = new ScoreCalculator();
            Console.WriteLine("Players added.");

            StandardMessages.GameStarting();
            GameFlow gameFlow = new GameFlow(PlayerData.players);
            gameFlow.StartGame(); // Start the game using GameFlow

            Console.ReadKey();
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
                        Console.WriteLine($"Number of players: {players.Count}");
                    }

                }
                allPlayersComplete = PlayerData.players.All(p => PlayerData.IsScoreboardComplete(p));
                Console.WriteLine("Game Over. Final Score:");
                foreach (var player in players)
                {
                    int finalScore = scoreCalculator.CalculateTotalScore(player);
                    Console.WriteLine($"{player.Name}'s final score: {finalScore}");
                    Console.WriteLine("test");
                }
            }

               

            Console.ReadKey();
        }
    }
}
