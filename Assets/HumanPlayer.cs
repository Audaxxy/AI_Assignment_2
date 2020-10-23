using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : PlayerBase
   
{
    
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {

        

        
    }
    public void OnPlayerClick(Box box)
    {

        if (GameHandler.currentPlayer!=this || GameHandler.gameOver)  
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

                grid.checkWin();
                gameHandler.nextTurn();
            }
        }
        
    }
}
