using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceGame
{
    class Game
    {
        static void Main(string[] args)
        {

            // ---------- SETTING AND INSTANTIATING PLAYERS ---------- //
            //Print at the start
            Console.WriteLine("\t\t\t~~~~~~ 3-of-a-kind Dice Game Fun Simulator-O-Matic ~~~~~~");
            Console.WriteLine("\n\t\t\t\t\tWelcome!");
            Console.WriteLine("\t........");
            Console.WriteLine("\t\t................");
            Console.WriteLine("\t\t\t\t........................");
            Console.WriteLine("\t\t\t\t\t\t\t................................");

            //Menu Flag
            bool menuFlag = false;

            //Game while loop - loops back to start of game if an exception is caught
            while (!menuFlag)
            {
                //Players Exception Handler
                try
                {
                    //User enters number of players playing the game
                    Console.WriteLine("Please enter the number of players partaking (hint: numbers only)");
                    Console.Write("Number of Players: ");
                    int numberOfPlayers = Convert.ToInt32(Console.ReadLine());

                    //Manual exception makes sure minimum players has to be 1
                    if (numberOfPlayers < 1)
                    {
                        throw new System.ArgumentException("The number of players cannot be zero", "numberofPlayers");
                    }

                    //An array of players is created based on the chosen amount of players partaking in the game
                    Player[] Players = new Player[numberOfPlayers];

                    //Instantiate players and assign name
                    for (int i = 0; i < Players.Length; i++)
                    {
                        //Player enters name
                        Console.WriteLine("Please enter name for Player {0}", i + 1);
                        Console.Write("Name: ");
                        string playerName = Console.ReadLine();

                        //Player object is instantiated and assigned name
                        Players[i] = new Player(playerName);
                    }


                    // ---------- END OF SETTING AND INSTANTIATING PLAYERS ---------- //



                    // ---------- INSTANTIATING DICE ---------- //

                    //Create a random object for the random dice roll
                    Random diceRandomiser = new Random();

                    //Create an array for the 5 dice
                    Die[] fiveDice = new Die[5];

                    //Instantiating the dice
                    for (int i = 0; i < fiveDice.Length; i++)
                    {
                        fiveDice[i] = new Die(diceRandomiser);
                    }

                    // ---------- END OF INSTANTIATING DICE ---------- //



                    // ---------- GAMEPLAY ---------- //

                    //Attributes
                    bool isGameOver = false;

                    //Instantiate a new game and assigning the turn number to 1
                    Statistics.GameStatistics NewGame = new Statistics.GameStatistics(); /* Instantiate new game */                                
                    NewGame.totalNumRoll = new int[6]; /* this array will hold the number of times each die number has been rolled */
                    NewGame.turnNumber = 1; /* starting turn number */

                    //User can enter the score condition 
                    //Here you can assign the value the player(s) have to reach in order to win
                    Console.WriteLine();
                    Console.WriteLine("Please enter the value players have to reach to win (hint: numbers only)");
                    Console.Write("Value: ");
                    int scoreCondition = Convert.ToInt32(Console.ReadLine());

                    //Manual exception makes sure minimum players has to be 1
                    if (scoreCondition < 1)
                    {
                        throw new System.ArgumentException("The score condition cannot be zero", "scoreCondition");
                    }

                    //Assigning score condition to game
                    NewGame.scoreCondition = scoreCondition;



                    //Game rules are printed to console
                    Console.WriteLine("\nPlayers take turns rolling five dice and score for three-of-a-kind or better.");
                    Console.WriteLine("If a player only has two-of-a-kind, they may re -throw the remaining dice in an attempt to improve the remaining dice values.");
                    Console.WriteLine("If no matching numbers are rolled after two rolls, the player scores 0.");
                    Console.WriteLine("\nSCORING");
                    Console.WriteLine("\t3-of-a-kind: 3 points");
                    Console.WriteLine("\t4-of-a-kind: 6 points");
                    Console.WriteLine("\t5-of-a-kind: 12 points");
                    Console.WriteLine("\nThe player who reaches {0} points wins the game.", NewGame.scoreCondition);
                    Console.WriteLine("Press enter to continue...");

                    //While loop runs game until a player wins
                    while (isGameOver == false)
                    {
                        Console.ReadLine();
                        Console.WriteLine("\n------------------------------------------------------------");
                        Console.WriteLine("\t\t\tTurn Number {0}", NewGame.turnNumber);

                        //Iterating through players
                        for (int i = 0; i < Players.Length; i++)
                        {
                            Console.WriteLine("------------------------------------------------------------");
                            Console.ReadLine();
                            Console.WriteLine("Player {0}'s turn", Players[i].GetName());
                            Console.WriteLine("\n\tPlayer {0} rolls the dice!", Players[i].GetName());

                            //Dice roll for loop - all five dice are rolled
                            for (int j = 0; j < fiveDice.Length; j++)
                            {
                                fiveDice[j].Roll();
                            }

                            //Dice Get Roll Number - Roll number is printed to console
                            Console.WriteLine("\n\t[{0}]\t[{1}]\t[{2}]\t[{3}]\t[{4}]\n", fiveDice[0].GetRollNumber(), fiveDice[1].GetRollNumber(), fiveDice[2].GetRollNumber(), fiveDice[3].GetRollNumber(), fiveDice[4].GetRollNumber());

                            //Roll is then checked to see if points have been scored
                            NewGame.CheckRoll(fiveDice, Players[i]);

                            //Roll is added the the dice roll history
                            NewGame.AddToHistory(fiveDice, Players[i]);

                            //Print players total score
                            Console.WriteLine("{0} total score is: {1}", Players[i].GetName(), Players[i].GetScore());

                            //Check if game is over
                            //The int within the if condition can be changed in order to change score
                            if (Players[i].GetScore() >= NewGame.scoreCondition)
                            {
                                Console.WriteLine("\n\t\t*^*^*^*^*^*^*^*^*^*^*^*^\tHOORAY!!\t*^*^*^*^*^*^*^*^*^*^*^*^");
                                Console.WriteLine("\nPlayer {0} {1} has won the game with {2} points!", i + 1, Players[i].GetName(), Players[i].GetScore());
                                Console.WriteLine("\nPress enter to see roll history and stats...");
                                Console.ReadLine();

                                //Show History - statistics are printed
                                NewGame.ShowHistory(Players, i);
                                NewGame.ShowTotalDiceNum();

                                //Bool flag is assigned true and ends game
                                isGameOver = true;
                                break;
                            }
                        }
                        //Increment turn number
                        NewGame.turnNumber++;

                    }

                    // ---------- END OF GAMEPLAY ---------- //


                    // ---------- GAME OVER ---------- //
                    Console.WriteLine("\nThe game is over.");
                    Console.WriteLine("\nThe application will now close...");
                    menuFlag = true;

                    //Debug Mode
                    Console.ReadLine();

                }
                //End of Try section of Exception Handler - any exceptions will be handled here and the catch message will print
                catch (Exception ex)
                {
                    Console.WriteLine("\nERROR: an exception was caught\nException: {0}", ex.Message);
                    Console.WriteLine();
                    menuFlag = false;
                }

                // ---------- END OF GAME OVER ---------- //

            }
            //end of while loop
        }
        //end of Main        
    }
    //end of class
}
// end of namespace