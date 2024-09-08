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
        private PlayerTurnHandler _turnHandler;
        private ScoreCalculator _scoreCalculator;

        public GameFlow(List<Player> players)
        {
            _players = players;
        }

        public void StartGame()
        {
            while (!_players.All(player => player.IsScoreboardComplete()))
            {
                foreach (var player in _players)
                {
                    if (!player.IsScoreboardComplete())
                    {
                        Console.WriteLine($"Player {player.Name}'s turn.");
                        PlayerTurnHandler turnHandler = new PlayerTurnHandler(player);  // Handle the player's turn
                        turnHandler.StartTurn();  // Start their turn
                    }
                }
            }

            // End the game
            EndGame();
        }

        private void EndGame()
        {
            Console.WriteLine("All players have completed their scoreboards. Game over!");
            foreach (var player in _players)
            {
                int finalScore = _scoreCalculator.CalculateTotalScore(player);
                Console.WriteLine($"{player.Name}'s final score: {finalScore}");
            }
        }
    }

}
