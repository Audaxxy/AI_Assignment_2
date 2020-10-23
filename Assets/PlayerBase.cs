using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MySymbol
{
    X,O
}

public class PlayerBase : MonoBehaviour
{
	private void Awake()
	{
		grid = GetComponent<Grid>();
		gameHandler = GetComponent<GameHandler>();
	}
	public Grid grid;
    public GameHandler gameHandler;
    public MySymbol mySymbol;
    
}

