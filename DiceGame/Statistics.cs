using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class Statistics
    {
        //This struct holds statistics of the game
        public struct GameStatistics
        {
            //Attributes
            public int turnNumber;
            public int scoreCondition;
            public int[] totalNumRoll; /* this array will hold the number of times each die number has been rolled */

            // ---------- METHODS ---------- //

            public void CheckRoll(Die[] fiveDice, Player currentPlayer)
            {
                //Attributes
                int[] currentRoll = new int[fiveDice.Length];

                int[] diceRepresentation = new int[6]; /*Each element represents a number on the dice */
                int mostCommon;
                int twoKindNumber;

                //for loop iterates through and assigns each roll number to the currentRoll array
                for (int i = 0; i < fiveDice.Length; i++)
                {
                    currentRoll[i] = fiveDice[i].GetRollNumber();

                    //Appropriate index of this array is incremented when a certain roll number is added to current roll
                    totalNumRoll[currentRoll[i] - 1]++;
                }

                //Foreach - increments element releated to dice roll
                foreach(int dice in currentRoll)
                {
                    diceRepresentation[dice - 1]++;
                }

                //.Max() method finds the amount of the most common number rolled
                mostCommon = diceRepresentation.Max();

                //Array.IndexOf finds the index of the most common roll
                twoKindNumber = (Array.IndexOf(diceRepresentation, mostCommon) + 1);

                //If 2-of-a-kid - reroll other dice
                if (mostCommon == 2)
                {
                    Console.WriteLine("You got 2-of-a-kid... Roll again!!");
                    Console.ReadLine();
                    CheckTwoOfAKid(fiveDice, currentPlayer, currentRoll, twoKindNumber, mostCommon, diceRepresentation);
                    return;
                }


                //RollScore method is calculated 
                RollScore(currentPlayer, mostCommon);

            }

            public void CheckTwoOfAKid(Die[] fiveDice, Player currentPlayer, int[] currentRoll, int twoKindNumber, int mostCommon, int[] diceRepresentation)
            {            
                //Dice that arent part of the two of a kid are rolled again
                for (int i = 0; i < fiveDice.Length; i++)
                {
                    //This if statement makes sure the two of a kind dice arent rolled again
                    if (currentRoll[i] == twoKindNumber)
                        continue;
                    else
                        fiveDice[i].Roll();
                }

                //for loop iterates through and assigns each roll number to the currentRoll array
                for (int i = 0; i < fiveDice.Length; i++)
                {
                    currentRoll[i] = fiveDice[i].GetRollNumber();

                    //Appropriate index of this array is incremented when a certain roll number is added to current roll
                    totalNumRoll[currentRoll[i] - 1]++;
                }

                //diceRepresentation array must be cleared before it is used again to prevent previous roll affecting the new representation
                Array.Clear(diceRepresentation, 0, diceRepresentation.Length);
                //Foreach - increments element releated to dice roll               
                foreach (int dice in currentRoll)
                {
                    diceRepresentation[dice - 1]++;
                }

                //.Max() method finds the amount of the most common number rolled
                mostCommon = diceRepresentation.Max();


                //Dice rolls are printed
                Console.WriteLine("\n\tPlayer {0} rolls the dice!", currentPlayer.GetName());
                Console.WriteLine("\n\t[{0}]\t[{1}]\t[{2}]\t[{3}]\t[{4}]\n", fiveDice[0].GetRollNumber(), fiveDice[1].GetRollNumber(), fiveDice[2].GetRollNumber(), fiveDice[3].GetRollNumber(), fiveDice[4].GetRollNumber());

                //RollScore method is then called again to check whether new dice throw has scored points
                RollScore(currentPlayer, mostCommon);
            }

            public void RollScore(Player currentPlayer, int mostCommon)
            {
                //If mostCommon number is 1, or is 2 (second roll) then the player does not score any points
                if(mostCommon < 3)
                {
                    Console.WriteLine("{0} didn't score any points :(", currentPlayer.GetName());
                }

                //Add score if a 3-of-a-kid, 4-of-a-kid or 5-of-a-kid is found
                if (mostCommon == 3)
                {
                    currentPlayer.AddScore(3);
                    Console.WriteLine("{0} scored 3!", currentPlayer.GetName());
                }

                if (mostCommon == 4)
                {
                    currentPlayer.AddScore(6);
                    Console.WriteLine("{0} scored 6!", currentPlayer.GetName());
                }

                if (mostCommon == 5)
                {
                    currentPlayer.AddScore(12);
                    Console.WriteLine("{0} scored 12!", currentPlayer.GetName());
                }

            }

            public void AddToHistory(Die[] fiveDice, Player currentPlayer)
            {
                //Attributes 
                int[] currentRoll = new int[fiveDice.Length];

                //for loop iterates through dice rolls and assigns to array
                for (int i = 0; i < fiveDice.Length; i++)
                {
                    currentRoll[i] = fiveDice[i].GetRollNumber();
                }

                //Array is then assigned to a List<int[]> called diceRollHistory
                currentPlayer.diceRollHistory.Add(currentRoll);

            }

            public void ShowHistory(Player[] Players, int winner)
            {
                //Attributes
                int turn = 1; /* int turn is used to print turn numbers */
                bool winningRoll = false;

                //print statistics
                Console.WriteLine("\n");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("{0} {1,35} {2,19}", "|**|", "Match Statistics", "|**|");
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("{0,10} {1,25} {2,18}", "|**|", "Dice Rolls", "|**|");
                Console.WriteLine();

                //This for loop loops through each turn
                for (int i = 0; i < Players[winner].diceRollHistory.Count; i++)
                {
                    //if statement checks whether this roll print is the winning roll
                    if(winningRoll == false)
                    {
                        //This for loop loops through the players
                        for (int j = 0; j < Players.Length; j++)
                        {
                            //If diceRollHistory has reached the winners last roll, then winningRoll is true and printing stops
                            if (i == Players[winner].diceRollHistory.Count - 1)
                            {
                                //This prints the player
                                Console.Write("Turn {0} Player {1}: ", turn, Players[j].GetName());
                                //This prints that players roll for the turn
                                PrintRollHistory(Players[j].diceRollHistory, i);
                                Console.WriteLine("\n");

                                //Checks if printed roll was the winners
                                if(Players[j] == Players[winner])
                                {
                                    winningRoll = true;
                                    return;
                                }

                            }

                            //This prints the player
                            Console.Write("Turn {0} Player {1}: ", turn, Players[j].GetName());
                            //This prints that players roll for the turn
                            PrintRollHistory(Players[j].diceRollHistory, i);
                            Console.WriteLine("\n");
                        }
                        turn++;
                    }
                }
            }

            public void ShowTotalDiceNum()
            {
                //this method prints out totalNumRoll
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Dice Frequencies");
                Console.WriteLine("These values include rolls and re-rolls\n");
                for (int i = 0; i < totalNumRoll.Length; i++)
                {
                    Console.WriteLine("Number {0} was rolled {1} time(s)", i + 1, totalNumRoll[i]);
                }
            }

            public void PrintRollHistory(List<int[]> diceRollHistory, int index)
            {

                //diceRoll holds the current roll
                int[] diceRoll = diceRollHistory[index];
                double averageThrow = 0;

                //foreach loops through each dice in the diceRoll
                foreach (int roll in diceRoll)
                {
                    //print roll and add to average
                    Console.Write("\t[{0}]", roll);
                    averageThrow = averageThrow + roll;
                }

                //Print average throw
                averageThrow = (averageThrow / diceRoll.Length);
                Console.Write("\tAverage throw: {0}", averageThrow);
                Console.WriteLine();          
            }
        }
    }
}