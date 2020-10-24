//100653593	
//Nathan Boldy
//10/23/20
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class responsible for managing the game. Would normally call it GameManager but Unity called dibs for some reason
public class GameHandler : MonoBehaviour
{
    public PlayerBase player1, player2;//The two players
    public static PlayerBase currentPlayer;//The player who is currently taking their turn
    public Text messageBox;//The win/loss message box
    public GameObject panel;//The symbol selection UI panel
   
    public static bool gameOver = true;
	private void Start()
	{
        player1 = GetComponent<HumanPlayer>();//Setting reference for player 1
        player2 = GetComponent<AIPlayer>();// Setting reference for player 2
      
        currentPlayer = player1; //Defaulting current player to player 1 for safety, overidden by user selection
	}
	void Update()
    {
            panel.SetActive(gameOver); //When gameover == true symbol selection will be visible, invisible otherwise
            messageBox.gameObject.SetActive(gameOver); //When gameover == true, win/loss message will be visible, invisible otherwise
    }
    public void nextTurn()//Goes to the next turn
    {

       BoxState winner= currentPlayer.grid.checkWin();//If the current player won, their symbol will have been returned by checkWin(), or if the board is full, resulting in a tie. Otherwise there has been no winner yet and the next turn will be initiated
        if (winner == BoxState.X)//If "X" won
        {
            messageBox.text = "X Wins!";//Change win/loss message
            currentPlayer.isFirstTurn = true;//Reset the isFirstTurn bool used for the AiPlaysFirst optimization
            gameOver = true; //ends the game, triggering popups
        }
        else if (winner == BoxState.O)//If "O" won
        {
            
            messageBox.text = "O Wins!";//Change win/loss message
            currentPlayer.isFirstTurn = true;//Reset the isFirstTurn bool used for the AiPlaysFirst optimization
            gameOver = true;//ends the game, triggering popups
        }
        else//Checks for a tie
        {
            bool isFull=true;//Is the board full?
            for (int i = 0; i < 3; i++)//Checks every box's boxState, if any of the boxes are empty, isFull will be false, meaninga tie has not been reached.
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentPlayer.grid.gridArray[i, j] == BoxState.Empty)//If any box is empty
                    {
                        isFull = false;//Grid is not full
                    }
                }
            }
            if (isFull)// If grid is indeed full, tie
            {
                messageBox.text = "Tie!"; //Change win/loss message
                currentPlayer.isFirstTurn = true; //Reset the isFirstTurn bool used for the AiPlaysFirst optimization
                gameOver = true; //ends the game, triggering popups
            }
        }

        // If the game has not yet been won, lost, or tied

        if (currentPlayer == player1)//if currently player1's turn, set currentplayer to player 2
        {
            currentPlayer = player2;
        }
        else if (currentPlayer == player2)//if currently player2's turn, set currentplayer to player 1
        {
            currentPlayer = player1;
        }
    }
    public void playX()//Called when player chooses to play as "X"
    {
        player1.mySymbol = MySymbol.X; //Sets human player symbol to X
        player2.mySymbol = MySymbol.O; //Sets AI player symbol to O
        currentPlayer = player2; //AI goes first
        currentPlayer.grid.ResetGrid();//Resets grid
        player2.isFirstTurn = true; //AI is first turn, setting isFirstTurn to true for usage in AiPlaysFirst optimization
        AiPlaysFirstOptimization(); //AiPlaysFirstOptimization
        gameOver = false;//Restarts the game, hides popup windows
    }
    public void playO() //Called when player chooses to play as "O"
    {
        player1.mySymbol = MySymbol.O; //Sets human player symbol to O
        player2.mySymbol = MySymbol.X; //Sets AI player symbol to X
        currentPlayer = player1; //Human goes first
        currentPlayer.grid.ResetGrid();// Resets grid
        player2.isFirstTurn = false; //AI is not first turn, setting isFirstTurn to false to disable AiPlaysFirst optimization
        gameOver = false; //Restarts the game, hides popup windows
    }
    public void AiPlaysFirstOptimization()//Optimization that prevents the AI from needlessly doing ~300k iterations to determine where to place it's first move.
    {
        currentPlayer.grid.UpdateGrid(0,BoxState.O);//Changes [0,0] grid  boxState to "O"
        currentPlayer.grid.boxes[0].GetComponent<Box>().boxText.text = BoxState.O.ToString();//Changes top left corner box text to "O"
        currentPlayer.grid.boxes[0].GetComponent<Box>().boxText.color = Color.blue;//Changes text color to blue
        currentPlayer.grid.boxes[0].GetComponent<Box>().boxState = BoxState.O;//Changes top left corner boxText to "O"
        currentPlayer.isFirstTurn = false;//Disables the tree bypass
        nextTurn();//Goes to human player's turn
    }
}
