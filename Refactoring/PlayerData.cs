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
        public static List<Player> players = new List<Player>();
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
                    // Error handling: display a message and reprompt
                    StandardMessages.InvalidPlayerCountEntry();
                }
            }

        }
        public static void PlayerName()
        {
            for (int i = 1; i <= playerCount; i++)
            {
                Console.Clear();
                Console.Write($"Enter name for player {i}: ");
                string name = Console.ReadLine();
                players.Add(new Player(name));

            }
        }
        public class Player
        {
            public string Name { get; private set; }
            public Player(string name)
            {
                Name = name;
            }
        }
    }
}
