using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    public class Player
    {
        //Attributes
        private string name;
        private int score;
        public List<int[]> diceRollHistory = new List<int[]>(); /* Each player will have their own roll history */

        //Parameterised Constructor
        public Player(string playerName)
        {
            name = playerName;
            score = 0;
        }

        // ---------- METHODS ---------- //

        //Set Player Name
        public void SetName(string playerName)
        {
            name = playerName;
        }

        //Get Player Name
        public string GetName()
        {
            return name;
        }

        //Set Player Score - Method adds on points to current score held in specific player object
        public void AddScore(int playerScore)
        {
            //playerScore is added to score
            score = score + playerScore;
        }
        
        //Get Player Score
        public int GetScore()
        {
            return score;
        }
    }
}