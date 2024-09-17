using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    internal class ScoreboardEntryCalculation
    {
            public static Dictionary<string, Func<int[], int>> ScoreFunctions { get; private set; }

            static ScoreboardEntryCalculation()
            {
                ScoreFunctions = new Dictionary<string, Func<int[], int>>
        {
            {"ones", dice => dice.Where(d => d == 1).Sum(d => 1)},
            {"twos", dice => dice.Where(d => d == 2).Sum(d => 2)},
            {"threes", dice => dice.Where(d => d == 3).Sum(d => 3)},
            {"fours", dice => dice.Where(d => d == 4).Sum(d => 4)},
            {"fives", dice => dice.Where(d => d == 5).Sum(d => 5)},
            {"sixes", dice => dice.Where(d => d == 6).Sum(d => 6)},
            {"one pair", dice => OnePairCalculation(dice)},
            {"two pairs", dice => TwoPairCalculation(dice)},
            {"three pairs", dice => ThreePairCalculation(dice)},
            {"3 same", dice => nOfAKindCalculation(dice, 3)},
            {"4 same", dice => nOfAKindCalculation(dice, 4)},
            {"5 same", dice => nOfAKindCalculation(dice, 5)},
            {"small straight", dice => SmallStraightValidator(dice)},
            {"large straight", dice => LargeStraigtValidator(dice)},
            {"full straight", dice => FullStraightValidator(dice)},
            {"hut 2+3", dice => HutCalculation(dice)},
            {"house 3+3", dice => HouseCalculation(dice)},
            {"tower 2+4", dice => TowerCalculation(dice)},
            {"chance", dice => dice.Sum()},
            {"maxi-yahtzee", dice => MaxiYahtzeeValidator(dice)}
        };
            }

            private static int OnePairCalculation(int[] dice)
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

            private static int TwoPairCalculation(int[] dice)
            {
                // Group the dice by their values
                var pairs = dice.GroupBy(d => d)
                                .Where(g => g.Count() >= 2)  // Select groups that have at least two of the same value
                                .Select(g => new { Value = g.Key, Count = g.Count() / 2 })  // Map to value and count of pairs
                                .ToList();

                // Check if there are at least two distinct pairs
                if (pairs.Count() >= 2)
                {
                    return pairs.OrderByDescending(p => p.Value)  // Sort pairs by value, high to low
                                .Take(2)  // Select the top two pairs
                                .Sum(p => p.Value * 2);  // Sum twice the value of each pair
                }

                return 0;
            }


            private static int ThreePairCalculation(int[] dice)
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

            private static int nOfAKindCalculation(int[] dice, int count)
            {
                return dice.GroupBy(d => d)
                           .Where(g => g.Count() >= count)
                           .Select(g => g.Key * count)
                           .FirstOrDefault();
            }

            private static int SmallStraightValidator(int[] dice)
            {
                var straight = new HashSet<int>(dice);
                if (new int[] { 1, 2, 3, 4, 5 }.All(straight.Contains))
                    return 15;
                return 0;
            }

            private static int LargeStraigtValidator(int[] dice)
            {
                var straight = new HashSet<int>(dice);
                if (new int[] { 2, 3, 4, 5, 6 }.All(straight.Contains))
                    return 20;
                return 0;
            }

            private static int FullStraightValidator(int[] dice)
            {
                return new HashSet<int>(dice).Count == 6 ? 21 : 0;
            }

            private static int HutCalculation(int[] dice)
            {
                var groups = dice.GroupBy(d => d).ToList();

                // Check for the presence of exactly one triplet and one pair
                var hasThreeOfAKind = groups.FirstOrDefault(g => g.Count() == 3);
                var hasPair = groups.FirstOrDefault(g => g.Count() == 2);

                if (hasThreeOfAKind != null && hasPair != null)
                {
                    // Score is calculated as the sum of all dice that are part of the full house
                    return hasThreeOfAKind.Key * 3 + hasPair.Key * 2;
                }

                // If there isn't one triplet and one pair, the score is zero
                return 0;
            }

            private static int HouseCalculation(int[] dice)
            {
                var groups = dice.GroupBy(d => d).ToList();
                return groups.Count(g => g.Count() >= 3) == 2 ? groups.Sum(g => g.Key * g.Count()) : 0;
            }

            private static int TowerCalculation(int[] dice)
            {
                var groups = dice.GroupBy(d => d).ToList();
                if (groups.Any(g => g.Count() == 4) && groups.Any(g => g.Count() == 2))
                    return groups.Sum(g => g.Key * g.Count());
                return 0;
            }

            private static int MaxiYahtzeeValidator(int[] dice)
            {
                if (dice == null || dice.Length == 0 || dice.All(d => d == 0))
                {
                    return 0;  // No valid roll, return 0
                }
                return dice.All(d => d == dice[0]) ? 100 : 0;
            }
        public static void CheckBonusEligibility(PlayerProperties player)
        {
            int? combinedScore = 0;

            combinedScore += player.PlayerCard["ones"] ?? 0;
            combinedScore += player.PlayerCard["twos"] ?? 0;
            combinedScore += player.PlayerCard["threes"] ?? 0;
            combinedScore += player.PlayerCard["fours"] ?? 0;
            combinedScore += player.PlayerCard["fives"] ?? 0;
            combinedScore += player.PlayerCard["sixes"] ?? 0;

            if (combinedScore >= 84)
            {
                player.BonusCheck = true;
            }
        }
        public int CalculateTotalScore(PlayerProperties player)
        {
            int totalScore = player.PlayerCard.Values.Sum(v => v.GetValueOrDefault());

            // If the player qualifies for the bonus, add bonus points
            if (player.BonusCheck)
            {
                totalScore += 100;  // Assuming a bonus of 100 points
            }

            return totalScore;
        }
    }
    }


