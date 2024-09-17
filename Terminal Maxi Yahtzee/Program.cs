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
            ScoreboardEntryCalculation scoreCalculator = new ScoreboardEntryCalculation();
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

                    if (!isComplete)
                    {
                        TurnHandler turnHandler = new TurnHandler(player);
                        Console.WriteLine($"Number of players: {players.Count}");
                    }

                }
                allPlayersComplete = PlayerData.players.All(p => PlayerData.IsScoreboardComplete(p));
                foreach (var player in players)
                {
                    Console.WriteLine($"{player.Name}'s Scoreboard:");
                }
            }

            while (true) //infinite loop to prevent accidental application closing
            {
            }
        }
    }
}
