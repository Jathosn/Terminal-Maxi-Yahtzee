using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class GameFlow
    {
        private List<Player> _players;
        private ScoreCalculator _scoreCalculator;

        public GameFlow(List<Player> players)
        {
            _players = players;
            _scoreCalculator = new ScoreCalculator();
        }

        public void StartGame()
        {
            while (!_players.All(player => player.IsScoreboardComplete()))
            {
                foreach (var player in _players)
                {
                    if (!player.IsScoreboardComplete())
                    {
                        Console.Clear();
                        PlayerTurnHandler turnHandler = new PlayerTurnHandler(player);
                        turnHandler.StartTurn();
                    }
                }
            }

            // End the game
            EndGame();
        }

        private void EndGame()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Game Over. Final Score:\n");
            Console.ResetColor();
            foreach (var player in _players)
            {
                int finalScore = _scoreCalculator.CalculateTotalScore(player);
                Console.WriteLine($"{player.Name}'s final score: {finalScore}");
            }
        }
    }
}
