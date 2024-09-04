using System;
using System.Collections.Generic;
using System.Linq;

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
        public void ChooseScoreCategory(int diceSum)
        {
            PrintPlayerCard();
            Console.WriteLine("Choose a category to score your dice sum:");
            string chosenCategory = Console.ReadLine().ToLower().Trim();

            if (PlayerCard.ContainsKey(chosenCategory) && PlayerCard[chosenCategory] == 0)
            {
                PlayerCard[chosenCategory] = diceSum;
                Console.WriteLine($"Updated {chosenCategory} with {diceSum} points.");
            }
            else
            {
                Console.WriteLine("Invalid category or already scored. Please try again.");
                ChooseScoreCategory(diceSum); // Retry if invalid input
            }
        }
    }
    class DiceThrower
    {
        public int[] DiceValues { get; private set; }
        private Random random;

        public DiceThrower()
        {
            DiceValues = new int[6];
            random = new Random();
            RollAllDice(); // Initialize all dice with random values
        }

        public void RollAllDice()
        {
            for (int i = 0; i < DiceValues.Length; i++)
            {
                DiceValues[i] = random.Next(1, 7);
            }
        }

        public void RollSpecificDice(bool[] diceToKeep)
        {
            for (int i = 0; i < DiceValues.Length; i++)
            {
                if (!diceToKeep[i]) // Only reroll dice that are not kept
                {
                    DiceValues[i] = random.Next(1, 7);
                }
            }
        }

        public void DisplayDice()
        {
            Console.WriteLine("Current dice values:");
            for (int i = 0; i < DiceValues.Length; i++)
            {
                Console.WriteLine($"Dice {i + 1}: {DiceValues[i]}");
            }
        }

        public bool[] GetDiceToKeep()
        {
            bool[] diceToKeep = new bool[6];
            Console.WriteLine("Enter 'y' to keep a die or 'n' to reroll it:");
            for (int i = 0; i < 6; i++)
            {
                Console.Write($"Keep dice {i + 1} [{DiceValues[i]}]? (y/n): ");
                char input = Console.ReadKey().KeyChar;
                Console.WriteLine();
                diceToKeep[i] = input == 'y' || input == 'Y';
            }
            return diceToKeep;
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
                Console.WriteLine($"{player.Name}'s turn to throw dice.");
                DiceThrower diceThrower = new DiceThrower();
                int throwCount = 3;

                for (int i = 0; i < throwCount; i++)
                {
                    diceThrower.DisplayDice();
                    if (i < throwCount - 1) // Allow rerolling only if it's not the last throw
                    {
                        bool[] diceToKeep = diceThrower.GetDiceToKeep();
                        diceThrower.RollSpecificDice(diceToKeep);
                    }
                }

                Console.WriteLine("Final dice values:");
                diceThrower.DisplayDice();
                int diceSum = diceThrower.DiceValues.Sum();
                player.ChooseScoreCategory(diceSum);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
