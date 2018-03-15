using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class Die
    {
        //Attributes
        private int numberOnTop;
        private Random numberRandomiser; 

        //Parameterised Constructor
        public Die(Random randomGenerator)
        {
            //Assigning random variable to number Randomiser
            numberRandomiser = randomGenerator;
        }

        // ---------- METHODS ---------- //

        //Roll Method
        public void Roll()
        {
            //numperOnTop is assigned a random number between 1 and 6
            numberOnTop = numberRandomiser.Next(1, 7);
        }

        //Get Roll Number
        public int GetRollNumber()
        {
            return numberOnTop;
        }
    }
}