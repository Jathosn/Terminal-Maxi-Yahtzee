﻿using System;
using System.Collections.Generic;

namespace Refactoring
{
    internal class Shortcuts
    {
        private static readonly Dictionary<string, string> CategoryShortcuts = new Dictionary<string, string>
    {
        { "on", "ones" },
        { "tw", "twos" },
        { "th", "threes" },
        { "fo", "fours" },
        { "fi", "fives" },
        { "si", "sixes" },
        { "op", "one pair" },
        { "tp", "two pairs" },
        { "thp", "three pairs" },
        { "3", "3 same" },
        { "4", "4 same" },
        { "5", "5 same" },
        { "ss", "small straight" },
        { "ls", "large straight" },
        { "fs", "full straight" },
        { "hu", "hut 2+3" },
        { "ho", "house 3+3" },
        { "to", "tower 2+4" },
        { "ch", "chance" },
        { "ma", "maxi-yahtzee" }
    };
        public static string GetFullCategoryName(string shorthand)
        {
            return CategoryShortcuts.ContainsKey(shorthand) ? CategoryShortcuts[shorthand] : shorthand;
        }
        public static void DisplayShorthandNotations()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var entry in CategoryShortcuts)
            {
                Console.WriteLine($"{entry.Key} => {entry.Value}");
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}
