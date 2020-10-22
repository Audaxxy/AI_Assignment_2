using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public PlayerBase player1, player2;
    public static PlayerBase currentPlayer;

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
        
    }
    public void nextTurn()
    {
        if (currentPlayer == player1)
        {
            currentPlayer = player2;
        }
        else if (currentPlayer == player2)
        {
            currentPlayer = player1;
            
        }
        
    }
}
