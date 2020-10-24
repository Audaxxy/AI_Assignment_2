//100653593	
//Nathan Boldy
//10/23/20
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AI and human players derive from this base class

public enum MySymbol //Easier identification of the symbol that represents a player denoted as either X or O
{
    X,O
}

public class PlayerBase : MonoBehaviour
{
	private void Awake()
	{
		grid = GetComponent<Grid>(); //Setting reference to the grid 
		gameHandler = GetComponent<GameHandler>(); // Setting reference to the gameHandler
	}
	
	public Grid grid; 
    public GameHandler gameHandler;
    public MySymbol mySymbol; 
	public bool isFirstTurn; //Used for AIPlaysFirst optimization
    
}

