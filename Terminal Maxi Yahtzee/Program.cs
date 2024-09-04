using System;
using System.Collections.Generic;
using System.Linq;

namespace Terminal_Maxi_Yahtzee
{
    class Player
    {
        public string Name { get; set; }
        public Dictionary<string, int?> PlayerCard { get; set; }

        public Player(string name)
        {
            Name = name;
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

        public void PrintPlayerCard()
        {
            Console.WriteLine($"{Name}'s Scorecard:");
            int maxKeyLength = PlayerCard.Keys.Max(key => key.Length);
            foreach (var entry in PlayerCard)
            {
                string scoreText = entry.Value.HasValue ? entry.Value.ToString() : "-";
                Console.WriteLine($"{entry.Key.PadRight(maxKeyLength)}: {scoreText}");
            }
        }
        public void ChooseScoreCategory(int[] diceValues)
        {
            PrintPlayerCard();
            Console.WriteLine("Choose a category to score:");
            string chosenCategory = Console.ReadLine().ToLower().Trim();

            if (PlayerCard.ContainsKey(chosenCategory) && !PlayerCard[chosenCategory].HasValue)
            {
                int score = ScoreCalculator.ScoreFunctions[chosenCategory](diceValues);
                PlayerCard[chosenCategory] = score;
                Console.WriteLine($"Updated {chosenCategory} with {score} points.");
            }
            else
            {
                Console.WriteLine("Invalid category or already scored. Please try again.");
                ChooseScoreCategory(diceValues); // Retry if invalid input
            }
        }

        public bool IsScoreboardComplete()
        {
            // Assuming no entry should remain 0 (assuming your game rules don't score zeros in a valid play)
            return PlayerCard.Values.All(score => score != 0);
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
    class ScoreCalculator
    {
        public static Dictionary<string, Func<int[], int>> ScoreFunctions { get; private set; }

        static ScoreCalculator()
        {
            ScoreFunctions = new Dictionary<string, Func<int[], int>>
        {
            {"ones", dice => dice.Where(d => d == 1).Sum(d => 1)},
            {"twos", dice => dice.Where(d => d == 2).Sum(d => 2)},
            {"threes", dice => dice.Where(d => d == 3).Sum(d => 3)},
            {"fours", dice => dice.Where(d => d == 4).Sum(d => 4)},
            {"fives", dice => dice.Where(d => d == 5).Sum(d => 5)},
            {"sixes", dice => dice.Where(d => d == 6).Sum(d => 6)},
            {"one pair", dice => GetHighestPairScore(dice)},
            {"two pairs", dice => GetTwoPairScore(dice)},
            {"three pairs", dice => GetThreePairsScore(dice)},
            {"3 same", dice => GetOfAKindScore(dice, 3)},
            {"4 same", dice => GetOfAKindScore(dice, 4)},
            {"5 same", dice => GetOfAKindScore(dice, 5)},
            {"small straight", dice => GetSmallStraightScore(dice)},
            {"large straight", dice => GetLargeStraightScore(dice)},
            {"full straight", dice => GetFullStraightScore(dice)},
            {"hut 2+3", dice => GetFullHouseScore(dice)},
            {"house 3+3", dice => GetTwoThreeOfAKindScore(dice)},
            {"tower 2+4", dice => GetTowerScore(dice)},
            {"chance", dice => dice.Sum()},
            {"maxi-yahtzee", dice => GetMaxiYahtzeeScore(dice)}
        };
        }

        private static int GetHighestPairScore(int[] dice)
        {
            // Group dice by value and filter groups where at least two dice share the same value
            var pairs = dice.GroupBy(d => d)
                            .Where(g => g.Count() >= 2)
                            .Select(g => new { Value = g.Key, Count = g.Count() })
                            .ToList();

            // If pairs exist, find the highest value pair and return twice its value
            if (pairs.Count > 0)
            {
                var highestPair = pairs.OrderByDescending(p => p.Value).First();
                return highestPair.Value * 2;
            }

            return 0;
        }

        private static int GetTwoPairScore(int[] dice)
        {
            var pairs = dice.GroupBy(d => d)
                            .Where(g => g.Count() >= 2)
                            .OrderByDescending(g => g.Key)
                            .Take(2)
                            .Select(g => g.Key * 2)
                            .Sum();

            return pairs == 0 || pairs / 2 == dice.Length ? 0 : pairs;
        }

        private static int GetThreePairsScore(int[] dice)
        {
            // Group by dice values and filter only groups that have exactly 2 of the same kind
            var groups = dice.GroupBy(d => d)
                             .Where(g => g.Count() == 2)
                             .ToList();

            // Ensure that there are exactly three groups (pairs)
            if (groups.Count == 3)
            {
                return groups.Sum(g => g.Key * 2);
            }

            return 0;
        }

        private static int GetOfAKindScore(int[] dice, int count)
        {
            return dice.GroupBy(d => d)
                       .Where(g => g.Count() >= count)
                       .Select(g => g.Key * count)
                       .FirstOrDefault();
        }

        private static int GetSmallStraightScore(int[] dice)
        {
            var straight = new HashSet<int>(dice);
            if (new int[] { 1, 2, 3, 4, 5 }.All(straight.Contains))
                return 15;
            return 0;
        }

        private static int GetLargeStraightScore(int[] dice)
        {
            var straight = new HashSet<int>(dice);
            if (new int[] { 2, 3, 4, 5, 6 }.All(straight.Contains))
                return 20;
            return 0;
        }

        private static int GetFullStraightScore(int[] dice)
        {
            return new HashSet<int>(dice).Count == 6 ? 21 : 0;
        }

        private static int GetFullHouseScore(int[] dice)
        {
            var groups = dice.GroupBy(d => d).ToList();
            if (groups.Any(g => g.Count() == 2) && groups.Any(g => g.Count() == 3))
                return groups.Sum(g => g.Key * g.Count());
            return 0;
        }

        private static int GetTwoThreeOfAKindScore(int[] dice)
        {
            var groups = dice.GroupBy(d => d).ToList();
            return groups.Count(g => g.Count() >= 3) == 2 ? groups.Sum(g => g.Key * g.Count()) : 0;
        }

        private static int GetTowerScore(int[] dice)
        {
            var groups = dice.GroupBy(d => d).ToList();
            if (groups.Any(g => g.Count() == 4) && groups.Any(g => g.Count() == 2))
                return groups.Sum(g => g.Key * g.Count());
            return 0;
        }

        private static int GetMaxiYahtzeeScore(int[] dice)
        {
            return dice.All(d => d == dice[0]) ? 50 : 0;
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

            bool allScoreboardsComplete = false;
            while (!allScoreboardsComplete)
            {
                allScoreboardsComplete = true; // Assume all are complete unless found otherwise

                foreach (Player player in players)
                {
                    Console.WriteLine($"\n{player.Name}'s turn to throw dice. \n");
                    DiceThrower diceThrower = new DiceThrower();
                    int throwCount = 3;  // Total number of throws allowed

                    for (int i = 0; i < throwCount; i++)
                    {
                        Console.WriteLine($"Throw {i + 1}:\n");
                        diceThrower.DisplayDice();

                        // Calculate and display throws remaining
                        int throwsRemaining = throwCount - i - 1;
                        if (throwsRemaining > 0)
                        {
                            Console.WriteLine($"You have {throwsRemaining} throws remaining.");
                            bool[] diceToKeep = diceThrower.GetDiceToKeep();
                            diceThrower.RollSpecificDice(diceToKeep);
                        }
                        else
                        {
                            Console.WriteLine("No throws remaining.");
                        }
                    }

                    Console.WriteLine("Final dice values:");
                    diceThrower.DisplayDice();

                    // Now passing the array of dice values instead of their sum
                    player.ChooseScoreCategory(diceThrower.DiceValues);
                    Console.WriteLine();
                    allScoreboardsComplete = false;
                }
            }

            Console.WriteLine("Game Over! Final Scores:");
            foreach (Player player in players)
            {
                player.PrintPlayerCard();
            }

            Console.ReadLine();
        }
    }
}
