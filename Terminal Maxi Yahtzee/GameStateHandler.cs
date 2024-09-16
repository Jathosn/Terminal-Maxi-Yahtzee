using System.Collections.Generic;
using System.Linq;

namespace Refactoring
{
    public class GameStateHandler
    {
        public List<PlayerProperties> Players { get; private set; }
        private int currentPlayerIndex = 0;

        public GameStateHandler(List<PlayerProperties> players)
        {
            Players = players;
        }

        public PlayerProperties GetCurrentPlayer()
        {
            return Players[currentPlayerIndex];
        }

        public void MoveToNextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
        }

        public bool AllScoreboardsComplete()
        {
            return Players.All(p => p.IsScoreboardComplete());
        }
    }

}
