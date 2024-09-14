using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    public class Player
    {
        // Player properties
        public string Name { get; private set; }
        public Dictionary<string, int?> PlayerCard { get; private set; }
        public int AvailableThrows { get; set; }
        public bool BonusCheck { get; set; }

        // Constructor
        public Player(string name)
        {
            Name = name;
            AvailableThrows = 3; // Initial available throws
            InitializePlayerCard(); // Initializes the scoreboard
        }

        // Initialize the player's scoreboard
        private void InitializePlayerCard()
        {
            PlayerCard = new Dictionary<string, int?>
            {
                { "ones", 85 },
                { "twos", null },
                { "threes", 0 },
                { "fours", 0 },
                { "fives", 0 },
                { "sixes", 0 },
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
            BonusCheck = false;
        }

        public bool IsScoreboardComplete()
        {
            return PlayerCard.Values.All(score => score.HasValue);
        }
        public void ChooseScoreCategory(int[] diceValues)
        {
            // Check if the player skipped their turn (diceValues is null)
            bool turnSkipped = diceValues == null;

            PlayerData.PrintPlayerCard(this);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nTurn ended: Enter category\n");
            Console.ResetColor();
            string inputCategory = Console.ReadLine().ToLower().Trim();
            Console.Clear();

                inputCategory = Shortcuts.GetFullCategoryName(inputCategory); // Convert shorthand to full name


            if (PlayerCard.ContainsKey(inputCategory) && !PlayerCard[inputCategory].HasValue)
            {
                int score = turnSkipped ? 0 : ScoreCalculator.ScoreFunctions[inputCategory](diceValues); // Set score to 0 if no dice were rolled
                PlayerCard[inputCategory] = score;
                Console.ForegroundColor = ConsoleColor.White;
                Console.ResetColor();
                ScoreCalculator.CheckBonusEligibility(this);
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid category or already scored. Please try again.");
                Console.ResetColor();
                ChooseScoreCategory(diceValues); // Retry if invalid input
            }
        }
    }
}
