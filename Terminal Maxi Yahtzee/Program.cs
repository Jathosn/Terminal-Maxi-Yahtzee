﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Terminal_Maxi_Yahtzee
{
    class PlayerCard
    {

        static Dictionary<string, int> playerCard = new Dictionary<string, int>
            {
                { "ones", 0 },
                { "twos", 0 },
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
        public static void PrintPlayerCard()
        {
            foreach (KeyValuePair<string, int> entry in playerCard)
            {
                Console.WriteLine("{0}: {1}", entry.Key, entry.Value);
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            PlayerCard.PrintPlayerCard();
            Console.ReadLine();
        }
    }
}
