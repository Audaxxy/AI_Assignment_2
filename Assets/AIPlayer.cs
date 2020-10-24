//100653593	
//Nathan Boldy
//10/23/20
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that handles the AI player
public class AIPlayer : PlayerBase//Derived from PlayerBase
{
	Vector2Int bestBox;//Stores coordinates of the best box

	int bestResult;//Stores the integer value of the best result

	public int numberOfIterations; //Stores the number of iterations the AI goes through to find the best box, used for debugging and optimization
	
	void Update()
	{

		if (GameHandler.currentPlayer != this || GameHandler.gameOver||isFirstTurn)// If not the AI's turn, or the game is over, or the AI is taking the first turn of the game, return. This will make the AIPlaysFirst Optimization override the standard procedure
		{
			return;
		}
		numberOfIterations = 0;//Reset iterations debugging
		bestBox = Vector2Int.zero;//Reset bestBox to [0,0]
		bestResult = -2;//Reset best result to -2


		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (grid.gridArray[i, j] != BoxState.Empty)//If the state of the box is X or O , continue
				{
					continue;
				}

				grid.gridArray[i, j] = (mySymbol == MySymbol.X) ? BoxState.X : BoxState.O;//can assume box is empty, Ternary operator to denote if the AI symbol is X, set the correpsonding boxState to X, if the AI symbol is O, set the correpsonding boxState to O
				
				int result = Move((mySymbol == MySymbol.X) ? BoxState.O : BoxState.X, bestResult);//This is where Move() is run. Move returns the bestMove as an integer value which result is then set to. Using ternary once more

				if (result > bestResult)//If the result integer of the last move is greater than the previous best result
				{
					bestResult = result; // Make the new bestResult the result
					bestBox = new Vector2Int(i, j); // set the bestBox as the current coordinate
				}
				grid.gridArray[i, j] = BoxState.Empty;//Reset the grid to how it was before tampering
			}
		}
		grid.gridArray[bestBox.x, bestBox.y] = (mySymbol == MySymbol.X) ? BoxState.X : BoxState.O; //Set script side bestBox's state to the AI's symbol
		grid.boxes[bestBox.x + bestBox.y * 3].GetComponent<Box>().boxText.text = mySymbol.ToString();//Set the Unity side bestBox's text to be the AI's symbol, either "X" or "O"

		if (mySymbol == MySymbol.X)// If AI is X
		{
			grid.boxes[bestBox.x + bestBox.y * 3].GetComponent<Box>().boxText.color = Color.red;//change text color to red
		}
		else if (mySymbol == MySymbol.O) // If AI is O
		{
			grid.boxes[bestBox.x + bestBox.y * 3].GetComponent<Box>().boxText.color = Color.blue;//change text color to blue
		}

			grid.boxes[bestBox.x + bestBox.y * 3].GetComponent<Box>().boxState = grid.gridArray[bestBox.x, bestBox.y]; //Set the Unity side bestBox's Unity scene state to the state of the bestBox

		gameHandler.nextTurn();// Start the Human player's turn. Function defined in GameHandler.cs
	}
	public int Move(BoxState symbol,int previousBestResult)//How the AI makes a move, takes in a Boxstate to denote which symbol the AI is playing as, as well as the previousBestResult for alpha beta pruning
	{
		int moveNum = 0;// Keeps track of number of empty boxes

		numberOfIterations++; //used for debugging optimizations

		//if symbol is the same as my symbol, set inital best move to -2, otherwise set to 2
		int bestMove = ((symbol == BoxState.X) == (mySymbol == MySymbol.X)) ? -2 : 2;//Create bestMove and initialize with either 2 or -2
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				if (grid.gridArray[i, j] != BoxState.Empty)//If the state of the box is X or O , continue
				{
					continue;
				}

				moveNum++; //Add to number of empty boxes

				int result = 0;//Initializing result integer

				grid.gridArray[i, j] = symbol; //Setting boxState of given grid coordinate to designated value of symbol

				BoxState winner = grid.checkWin();//Checking if a win condition has been reached, CheckWin() is defined in Grid.cs
				if (winner == BoxState.X)// If X wins
				{
					if (mySymbol == MySymbol.X) //If AI is X, good move
					{
						result = 1; 
					}
					else if (mySymbol == MySymbol.O) // If AI is O, bad move
					{
						result = -1;
					}
				}

				else if (winner == BoxState.O)// If O wins
				{
					if (mySymbol == MySymbol.O) //If AI is O, good move
					{
						result = 1;
					}
					else if (mySymbol == MySymbol.X) //If AI is X, good moves
					{
						result = -1;
					}
				}
				else // If winner is state "empty"
				{
					//Ternary operator, swaps current symbol for next recursion
					result = Move((symbol == BoxState.X) ? BoxState.O : BoxState.X,bestMove);
				}
				grid.gridArray[i, j] = BoxState.Empty;//Reset the grid to how it was before tampering

				if (symbol == BoxState.X) // If AI is simulating X's turn
				{
					if (mySymbol == MySymbol.X) //If AI is X
					{
						bestMove = Mathf.Max(bestMove, result); //Take the best result for X
						if (bestMove > previousBestResult)// OPTIMIZATION ALPHA BETA PRUNING, if bestMove is better than previousMove, which we can assume wants the worst move, skip rest of branch
						{
							return bestMove;
						}
					}
					else if (mySymbol == MySymbol.O) //If AI is O
					{
						bestMove = Mathf.Min(bestMove, result);//Take the worst result for O
						if (bestMove < previousBestResult) // OPTIMIZATION ALPHA BETA PRUNING, if bestMove is worse than previousMove, which we can assume wants the best move, skip rest of branch
						{
							return bestMove;
						}
					}
				}
				if (symbol == BoxState.O) // If simulating O's turn
				{
					if (mySymbol == MySymbol.O) //If AI is O
					{
						bestMove = Mathf.Max(bestMove, result); //Take the best result for O
						if (bestMove > previousBestResult) // OPTIMIZATION ALPHA BETA PRUNING, if bestMove is better than previousMove, which we can assume wants the worst move, skip rest of branch
						{
							return bestMove;
						}
					}
					else if (mySymbol == MySymbol.X) //If AI is X
					{
						bestMove = Mathf.Min(bestMove, result); //Take the worst result for X
						if (bestMove < previousBestResult) // OPTIMIZATION ALPHA BETA PRUNING, if bestMove is worse than previousMove, which we can assume wants the best move, skip rest of branch
						{
							return bestMove;
						}
					}
				}


			}
		}
		if (moveNum == 0)// If number of moves is 0, game is tied
		{
			return 0;
		}
		else//Otherwise, return best or worst result
		{
			return bestMove;
		}




	}
	

}
