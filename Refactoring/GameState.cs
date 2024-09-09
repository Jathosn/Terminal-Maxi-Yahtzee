using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class GameState
    {
        public List<Player> Players { get; private set; }
        private int currentPlayerIndex = 0;

        public GameState(List<Player> players)
        {
            Players = players;
        }

        public Player GetCurrentPlayer()
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
