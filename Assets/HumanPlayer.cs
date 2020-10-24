//100653593	
//Nathan Boldy
//10/23/20
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script which handles the Human player
public class HumanPlayer : PlayerBase//Derived from PlayerBase
{
    public void OnPlayerClick(Box box)//Called via Unity whenever a "Box" Button is clicked, passes reference of itself to script
    {
        if (GameHandler.currentPlayer!=this || GameHandler.gameOver)  //If not this player's turn or the game is over, return
        {
            return;
        }
        
        else // if the game is in play and it is the human's turn
        {
            if (box.boxState == BoxState.Empty) // If the "Box"'s current state is still empty (unoccupied)
            {
                box.boxText.text = mySymbol.ToString(); // Set the text of the "Box" to the character's corresponding symbol

                if (mySymbol == MySymbol.X)// If Human player is "X" 
                {
                    box.boxText.color = Color.red;//cahgne text color to red
                    box.boxState = BoxState.X;// Change the editor state of Box to "X"
                    grid.UpdateGrid(box.boxIdentifier, BoxState.X); //  Update the state of the box to "X". UpdateGrid() is defined in Grid.cs
                }
                else// If human player is "O"
                {
                    box.boxText.color = Color.blue; // change text color to blue
                    box.boxState = BoxState.O;// Change the editor state of Box to "O"
                    grid.UpdateGrid(box.boxIdentifier, BoxState.O); //  Update the state of the box to "O". UpdateGrid() is defined in Grid.cs
                }

                grid.checkWin(); //Check for a win
                gameHandler.nextTurn(); //Start player's turn.
            }
        }
        
    }
}
