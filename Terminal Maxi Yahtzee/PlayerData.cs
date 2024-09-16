using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Refactoring
{
    internal class PlayerData
    {
        public static int playerCount;
        public static List<PlayerProperties> players = new List<PlayerProperties>();

        public static void PlayerCount()
        {
            while (true)
            {
                Console.Write("Please input the number of players: ");
                string input = Console.ReadLine();

                // Try to parse the input and ensure it is a positive integer greater than 0
                if (int.TryParse(input, out playerCount) && playerCount > 0)
                {
                    break;  // Valid input, exit the loop
                }
                else
                {
                    Console.Clear();
                    // Error handling: display a message and reprompt
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a positive integer greater than 0.");
                    Console.ResetColor();
                }
            }

        }
        public static void PlayerName()
        {
            for (int i = 1; i <= playerCount; i++)
            {
                string name = "";

                // Loop until a valid name is entered
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.Clear();
                    Console.Write($"Enter name for player {i}: ");
                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input. Please enter a positive integer greater than 0.");
                        Console.ResetColor();
                    }
                }

                Console.Clear();

                // Add the player to the static list
                players.Add(new PlayerProperties(name));
            }
        }
        public static void PrintPlayerCard(PlayerProperties player)
        {
            int maxKeyLength = player.PlayerCard.Keys.Max(key => key.Length);
            foreach (var entry in player.PlayerCard)
            {
                string scoreText = entry.Value.HasValue ? entry.Value.ToString() : "-";
                Console.WriteLine($"{entry.Key.PadRight(maxKeyLength)}: {scoreText}");
            }
        }
        public static bool IsScoreboardComplete(PlayerProperties player)
        {
            // Check if all values in PlayerCard are not null (i.e., all categories are filled)
            return player.PlayerCard.Values.All(score => score.HasValue);
        }
    }
}
