using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : PlayerBase
   
{
    public GameHandler gameHandler;
    public Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<Grid>();
        gameHandler = GetComponent<GameHandler>();
        
    }

    // Update is called once per frame
    void Update()
    {

        

        
    }
    public void OnPlayerClick(Box box)
    {

        if (GameHandler.currentPlayer!=this)  
        {
            return;
        }
        else
        {
            if (box.boxState == BoxState.Empty)
            {

                
                box.boxText.text = mySymbol.ToString();
                

                if (mySymbol == MySymbol.X)
                {
                    box.boxState = BoxState.X;
                    grid.UpdateGrid(box.boxIdentifier, BoxState.X);
                }
                else
                {
                    box.boxState = BoxState.O;
                    grid.UpdateGrid(box.boxIdentifier, BoxState.O);
                }


                gameHandler.nextTurn();
            }
        }
        
    }
}
