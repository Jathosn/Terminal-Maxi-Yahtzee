﻿using System;
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
        }

        public void StartTurn()
        {
            if (!_player.IsScoreboardComplete())
            {
                bool decisionMade = false;

                Console.Clear();
                Console.WriteLine($"It's your turn, {_player.Name}\n");
                PlayerData.PrintPlayerCard(_player);
                Console.WriteLine($"\u001b[38;2;255;150;0m\nYou have {_player.AvailableThrows} throw(s) available.\u001b[0m \n");
                Console.WriteLine("Press 'ENTER' to throw\nPress 'S' to view scoreboard\nPress 'E' to end turn\nPress 'H' to view shorthand notations");

                while (!decisionMade)
                {
                    bool turnSkipped = false;
                    var keyPress = Console.ReadKey(true).Key;

                    switch (keyPress)
                    {
                        case ConsoleKey.Enter:
                            Console.Clear();
                            decisionMade = true;  // Proceed to dice rolling
                            break;

                        case ConsoleKey.S:
                            Console.Clear();
                            ViewScoreboard();
                            Console.WriteLine($"\u001b[38;2;255;150;0m\nYou have {_player.AvailableThrows} throw(s) available.\u001b[0m\n");
                            Console.WriteLine("Press 'ENTER' to throw\nPress 'S' to view scoreboard\nPress 'E' to end turn\nPress 'H' to view shorthand notations");
                            decisionMade = false;  // Stay in the input loop
                            break;

                        case ConsoleKey.H:
                            Console.Clear();
                            Shortcuts.DisplayShorthandNotations();
                            Console.WriteLine($"\u001b[38;2;255;150;0mYou have {_player.AvailableThrows} throw(s) available.\u001b[0m \n");
                            Console.WriteLine("Press 'ENTER' to throw\nPress 'S' to view scoreboard\nPress 'E' to end turn\nPress 'H' to view shorthand notations");
                            decisionMade = false;  // Stay in the input loop
                            break;

                        case ConsoleKey.E:
                            Console.Clear();
                            EndTurnEarly();
                            decisionMade = true;
                            turnSkipped = true;
                            break;
                    }

                    if (turnSkipped)
                    {
                        continue;  // Skip to the next player if the turn was skipped
                    }

                    if (decisionMade)  // Only proceed to dice rolling if Enter was pressed
                    {
                        int currentThrows = _player.AvailableThrows;
                        DiceThrower diceThrower = new DiceThrower();
                        int throwCount = _player.AvailableThrows;
                        bool endTurn = false;

                        for (int i = 0; i < throwCount; i++)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine($"{diceThrower.GetDiceValuesAsString()}\n");
                            Console.ResetColor();

                            int throwsRemaining = throwCount - i - 1;
                            if (throwsRemaining > 0)
                            {
                                Console.WriteLine($"\u001b[38;2;255;150;0mYou have {throwsRemaining} throw(s) remaining, {_player.Name}\u001b[0m \n");
                                decisionMade = false;
                                Console.WriteLine("Press 'ENTER' to continue");
                                Console.WriteLine("Press 'S' to view scoreboard");
                                Console.WriteLine("Press 'E' to end turn");
                                Console.WriteLine("Press 'H' to view shorthand notations");

                                while (!decisionMade)
                                {
                                    keyPress = Console.ReadKey(true).Key;

                                    if (keyPress == ConsoleKey.S)
                                    {
                                        Console.Clear();
                                        ViewScoreboard();
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine($"\n{diceThrower.GetDiceValuesAsString()}");
                                        Console.WriteLine($"\u001b[38;2;255;150;0m\nYou have {throwsRemaining} throw(s) available.\u001b[0m \n");
                                        Console.WriteLine("Press 'ENTER' to throw\nPress 'S' to view scoreboard\nPress 'E' to end turn\nPress 'H' to view shorthand notations");
                                        decisionMade = false;  // Stay in the input loop
                                    }
                                    else if (keyPress == ConsoleKey.H)
                                    {
                                        Console.Clear();
                                        Shortcuts.DisplayShorthandNotations();
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine($"{diceThrower.GetDiceValuesAsString()}");
                                        Console.WriteLine($"\u001b[38;2;255;150;0m\nYou have {throwsRemaining} throw(s) available.\u001b[0m \n");
                                        Console.WriteLine("Press 'ENTER' to throw\nPress 'S' to view scoreboard\nPress 'E' to end turn\nPress 'H' to view shorthand notations");
                                        decisionMade = false;  // Stay in the input loop
                                    }
                                    else if (keyPress == ConsoleKey.E)
                                    {
                                        Console.Clear();
                                        _player.AvailableThrows = 3 + throwsRemaining;
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine($"You ended your turn early. {throwsRemaining} throws carried over to your next turn.\n");
                                        Console.ResetColor();
                                        decisionMade = true;
                                        endTurn = true;  // Set flag to true to indicate turn end
                                        break;
                                    }
                                    else if (keyPress == ConsoleKey.Enter)
                                    {
                                        decisionMade = true;
                                    }
                                }

                                if (endTurn)
                                {
                                    break;
                                }

                                // Continue rolling dice
                                int[] currentRoll = diceThrower.DiceValues;  // Get the current roll
                                bool[] diceToKeep = diceThrower.GetDiceToKeep(currentRoll);  // Ask player which dice to keep
                                diceThrower.RollSpecificDice(diceToKeep);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("No throws remaining.");
                            }
                        }

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Result:");
                        Console.WriteLine($"\n{diceThrower.GetDiceValuesAsString()}\n");
                        Console.ResetColor();
                        _player.ChooseScoreCategory(diceThrower.DiceValues);
                        Console.WriteLine();
                    }
                }
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
            PlayerData.PrintPlayerCard(_player);
        }

        private void EndTurnEarly()
        {
            _player.AvailableThrows += 3; // Save all 3 throws for the next turn
            Console.WriteLine($"{_player.Name} skipped their turn. 3 throws saved for later turns.");
            _player.ChooseScoreCategory(null);
        }
    }

}
