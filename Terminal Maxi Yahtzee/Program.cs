using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<PlayerProperties> players = new List<PlayerProperties>();
            StandardMessages.WelcomeMessage();
            PlayerData.PlayerCount();
            PlayerData.PlayerName();
            ScoreCalculator scoreCalculator = new ScoreCalculator();
            Console.WriteLine("Players added.");

            Console.WriteLine();
            GameFlow gameFlow = new GameFlow(PlayerData.players);
            gameFlow.GameInitializer(); // Start the game using GameFlow

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
                        TurnHandler turnHandler = new TurnHandler(player);
                        Console.WriteLine($"Handler escaped");
                        Console.WriteLine($"Number of players: {players.Count}");
                    }

                }
                allPlayersComplete = PlayerData.players.All(p => PlayerData.IsScoreboardComplete(p));
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.Name}'s Scoreboard:");
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Game Over. Final Score:\n");
                Console.ResetColor();
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
