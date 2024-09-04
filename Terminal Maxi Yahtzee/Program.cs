using System;
using System.Collections.Generic;

namespace Terminal_Maxi_Yahtzee
{
    class Player
    {
        public string Name { get; set; }
        public Dictionary<string, int> PlayerCard { get; set; }

        public Player(string name)
        {
            Name = name;
            PlayerCard = new Dictionary<string, int>
            {
                { "ones", 0 },
                { "twos", 0 },
                { "threes", 0 },
                { "fours", 0 },
                { "fives", 0 },
                { "sixes", 0 },
                { "to bonus", 84 },
                { "one pair", 0 },
                { "two pairs", 0 },
                { "three pairs", 0 },
                { "3 same", 0 },
                { "4 same", 0 },
                { "5 same", 0 },
                { "small straight", 0 },
                { "large straight", 0 },
                { "full straight", 0 },
                { "hut 2+3", 0 },
                { "house 3+3", 0 },
                { "tower 2+4", 0 },
                { "chance", 0 },
                { "maxi-yahtzee", 0 }
            };
        }

        public void PrintPlayerCard()
        {
            Console.WriteLine($"{Name}'s Scorecard:");
            int maxKeyLength = 0;
            foreach (var key in PlayerCard.Keys)
            {
                if (key.Length > maxKeyLength)
                {
                    maxKeyLength = key.Length;
                }
            }

            foreach (KeyValuePair<string, int> entry in PlayerCard)
            {
                Console.WriteLine($"{entry.Key.PadRight(maxKeyLength)}: {entry.Value}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            Console.Write("Enter the number of players: ");
            int playerCount = int.Parse(Console.ReadLine());

            for (int i = 1; i <= playerCount; i++)
            {
                Console.Write($"Enter name for player {i}: ");
                string name = Console.ReadLine();
                players.Add(new Player(name));
            }

            foreach (Player player in players)
            {
                player.PrintPlayerCard();
                Console.WriteLine(); // Add a space between players' cards for better readability.
            }

            Console.ReadLine();
        }
    }
}
