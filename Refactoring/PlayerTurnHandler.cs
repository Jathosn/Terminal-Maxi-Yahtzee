using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class PlayerTurnHandler
    {
        private Player _player;
        private DiceThrower _diceThrower;

        public PlayerTurnHandler(Player player)
        {
            _player = player;
            _diceThrower = new DiceThrower();
            StartTurn();
        }

        public void StartTurn()
        {
            bool decisionMade = false;
            bool turnSkipped = false;

            Console.Clear();
            Console.WriteLine($"It's your turn, {_player.Name}");
            PlayerData.PrintPlayerCard(_player);
            Console.WriteLine($"\u001b[38;2;255;150;0m\nYou have {_player.AvailableThrows} throw(s) available.\u001b[0m \n");
            Console.WriteLine("Press 'ENTER' to throw\nPress 'S' to view scoreboard\nPress 'E' to end turn\nPress 'H' to view shorthand notations");


            while (!decisionMade)

            {
                var keyPress = Console.ReadKey(true).Key;
                switch (keyPress)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        decisionMade = true;
                        break;
                    case ConsoleKey.S:
                        Console.Clear();
                        ViewScoreboard();
                        break;
                    case ConsoleKey.H:
                        Console.Clear();
                        ShowShorthandNotations();
                        break;
                    case ConsoleKey.E:
                        Console.Clear();
                        EndTurnEarly();
                        decisionMade = true;
                        turnSkipped = true;
                        break;
                }
                Console.Clear();
            }

        }

        private void RollDice()
        {
            Console.Clear();
            Console.WriteLine($"{_diceThrower.GetDiceValuesAsString()}\n");
            _player.AvailableThrows -= 1;
            if (_player.AvailableThrows > 0)
            {
                Console.WriteLine("Press 'ENTER' to reroll or 'E' to end turn.");
                var keyPress = Console.ReadKey(true).Key;
                if (keyPress == ConsoleKey.Enter)
                {
                    // Handle reroll logic
                    RollDice();
                }
            }
            // Dice rolling finished, prompt the player to choose score category
            _player.ChooseScoreCategory(_diceThrower.DiceValues);
        }

        private void ViewScoreboard()
        {
            Console.Clear();
            Console.WriteLine($"{_player.Name}'s Scoreboard:");
            PlayerData.PrintPlayerCard(_player);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ShowShorthandNotations()
        {
            Console.Clear();
            Shortcuts.DisplayShorthandNotations();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void EndTurnEarly()
        {
            _player.AvailableThrows += 3; // Save all 3 throws for the next turn
            Console.WriteLine($"{_player.Name} skipped their turn. 3 throws saved for later turns.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

}
