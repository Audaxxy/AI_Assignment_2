using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : PlayerBase
{
    public Grid grid;
    public GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        gameHandler = GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.currentPlayer != this)
        {
            return;
        }
       
    }
	public void Move()
	{
       



    }

}
