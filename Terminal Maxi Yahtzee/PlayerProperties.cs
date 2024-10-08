﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    public class PlayerProperties
    {
        // Player properties
        public string Name { get; private set; }
        public Dictionary<string, int?> PlayerCard { get; private set; }
        public int AvailableThrows { get; set; }
        public bool BonusCheck { get; set; }

        // Constructor
        public PlayerProperties(string name)
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
                { "ones", null },
                { "twos", null },
                { "threes", null },
                { "fours", null },
                { "fives", null },
                { "sixes", null },
                { "one pair", null },
                { "two pairs", null },
                { "three pairs", null },
                { "3 same", null },
                { "4 same", null },
                { "5 same", null },
                { "small straight", null },
                { "large straight", null },
                { "full straight", null },
                { "hut 2+3", null },
                { "house 3+3", null },
                { "tower 2+4", null },
                { "chance", null },
                { "maxi-yahtzee", null }
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
                int score = turnSkipped ? 0 : ScoreboardEntryCalculation.ScoreFunctions[inputCategory](diceValues); // Set score to 0 if no dice were rolled
                PlayerCard[inputCategory] = score;
                Console.ForegroundColor = ConsoleColor.White;
                Console.ResetColor();
                ScoreboardEntryCalculation.CheckBonusEligibility(this);
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
