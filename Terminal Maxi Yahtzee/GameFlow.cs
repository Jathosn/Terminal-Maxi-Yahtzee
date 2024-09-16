using System;
using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    public class GameFlow
    {
        private List<PlayerProperties> _players;
        private ScoreCalculator _scoreCalculator;

        public GameFlow(List<PlayerProperties> players)
        {
            _players = players;
            _scoreCalculator = new ScoreCalculator();
        }

        public void GameInitializer()
        {
            while (!_players.All(player => player.IsScoreboardComplete()))
            {
                foreach (var player in _players)
                {
                    if (!player.IsScoreboardComplete())
                    {
                        Console.Clear();
                        TurnHandler turnHandler = new TurnHandler(player);
                        turnHandler.NextPlayer();
                    }
                }
            }
            EndGameScoreCalculation();
        }

        private void EndGameScoreCalculation()
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
