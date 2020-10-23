using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public PlayerBase player1, player2;
    public static PlayerBase currentPlayer;
    public Text messageBox;
    public GameObject panel;
   
    public static bool gameOver = true;
	private void Start()
	{

        player1 = GetComponent<HumanPlayer>();
        player2 = GetComponent<AIPlayer>();
        //add switching
        currentPlayer = player1;
	}
	// Update is called once per frame
    //Needs to check for 3 in row and have end message
	void Update()
    {
       
            panel.SetActive(gameOver);
            messageBox.gameObject.SetActive(gameOver);
    }
    public void nextTurn()
    {

       BoxState winner= currentPlayer.grid.checkWin();
        if (winner == BoxState.X)
        {
            
            messageBox.text = "X Wins!";
            gameOver = true;
        }
        else if (winner == BoxState.O)
        {
            
            messageBox.text = "O Wins!";
            gameOver = true;
        }
        else
        {
            bool isFull=true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (currentPlayer.grid.gridArray[i, j] == BoxState.Empty)
                    {
                        isFull = false;
                    }
                }
            }
            if (isFull)
            {
               
                messageBox.text = "Tie!";
                gameOver= true;
            }
        }

        if (currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        else if (currentPlayer == player2)
        {
            currentPlayer = player1;
            
        }
        
    }
    public void playX()
    {
        player1.mySymbol = MySymbol.X;
        player2.mySymbol = MySymbol.O;
        currentPlayer = player2;
        currentPlayer.grid.ResetGrid();
        gameOver = false;
    }
    public void playO()
    {
        player1.mySymbol = MySymbol.O;
        player2.mySymbol = MySymbol.X;
        currentPlayer = player1;
        currentPlayer.grid.ResetGrid();
        gameOver = false;
    }
    

}
