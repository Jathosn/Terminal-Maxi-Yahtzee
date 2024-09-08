using System;
using System.Collections.Generic;

namespace Refactoring
{
    public class Player
    {
        // Player properties
        public string Name { get; private set; }
        public Dictionary<string, int?> PlayerCard { get; private set; }
        public int AvailableThrows { get; private set; }

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
        }
    }
}
