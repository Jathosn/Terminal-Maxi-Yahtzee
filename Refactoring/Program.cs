using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //List<Player> players = new List<Player>();
            StandardMessages.WelcomeMessage();
            PlayerData.PlayerCount();
            PlayerData.PlayerName();
            StandardMessages.GameStarting();
            Console.ReadKey();  // This will keep the console window open until a key is pressed
        }
    }
}
