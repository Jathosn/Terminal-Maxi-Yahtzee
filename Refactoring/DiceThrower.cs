using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring
{
    public class DiceThrower
    {
        public int[] DiceValues { get; private set; }
        private Random _random;

        public DiceThrower()
        {
            DiceValues = new int[6];
            _random = new Random();
            RollAllDice();
        }

        public void RollAllDice()
        {
            for (int i = 0; i < DiceValues.Length; i++)
            {
                DiceValues[i] = _random.Next(1, 7);
            }
        }

        public void RollSpecificDice(bool[] diceToKeep)
        {
            for (int i = 0; i < DiceValues.Length; i++)
            {
                if (!diceToKeep[i])
                {
                    DiceValues[i] = _random.Next(1, 7);
                }
            }
        }

        public string GetDiceValuesAsString()
        {
            return string.Join(", ", DiceValues);
        }

        public bool[] GetDiceToKeep(int[] currentRoll)
        {
            bool[] diceToKeep = new bool[currentRoll.Length];
            // Logic to handle dice keeping goes here...
            return diceToKeep;
        }
    }

}
