//100653593	
//Nathan Boldy
//10/23/20
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

//Script for the grid of boxes
public class Grid : MonoBehaviour
{
    public List <Button> boxes; //Contains references to all of the Unity scene buttons

    public BoxState[,] gridArray = new BoxState[3, 3]; //Creating a 3x3 array containing boxStates 

    
    void Start()
    {
        ResetGrid();//Resets the grid on start, essentially creating the base empty grid
    }

	public void ResetGrid()//Populates the entire grid with the "Empty" state, denoted they are unoccupied
	{
        
            for (int i = 0; i < 3; i++) // i represents X axis of grid
            {
                for (int j = 0; j < 3; j++) // j represents Y axis of grid
                {
                    gridArray[i, j] = BoxState.Empty;//set grid coordinate denoted by [i,j] to "empty"
                    
                }
            }

        for (int i = 0; i < 9; i++) //Resetting Unity side text on boxes, and unity side state
        {
            boxes[i].GetComponent<Box>().boxState = BoxState.Empty; //Sets unity box state to empty
            boxes[i].GetComponent<Box>().boxText.text = ""; //Set sthe box text to blank
        }

        
    }
	
    public BoxState checkWin()//Checks for a win condition
    {
        for (int i = 0; i < 3; i++)//simply iterating 1-3
        {
            if (gridArray[i, 0] == gridArray[i, 1] && gridArray[i, 1] == gridArray[i, 2] && gridArray[i, 2] != BoxState.Empty)//Horizontal Line check: If box 1 2 and 3 are the same boxState (that isn't empty), return the boxState of the line 
            {
                return gridArray[i, 0];//Will return either a boxState of "X" or "O"
            }
            if (gridArray[0, i] == gridArray[1, i] && gridArray[1, i] == gridArray[2, i] && gridArray[2, i] != BoxState.Empty)//Vertical Line check: If box 1 2 and 3 are the same boxState (that isn't empty), return the boxState of the line 
            {
                return gridArray[0, i];//Will return either a boxState of "X" or "O"
            }
        }
        if (gridArray[0, 0] == gridArray[1, 1] && gridArray[1, 1] == gridArray[2, 2] && gridArray[2, 2] != BoxState.Empty) //Diagonal Line check 1: If box 1 2 and 3 are the same boxState (that isn't empty), return the boxState of the line 
        {
            return gridArray[0, 0];//Will return either a boxState of "X" or "O"
        }
        if (gridArray[2, 0] == gridArray[1, 1] && gridArray[1, 1] == gridArray[0, 2] && gridArray[0, 2] != BoxState.Empty) //Diagonal Line check 2: If box 1 2 and 3 are the same boxState (that isn't empty), return the boxState of the line 
        {
            return gridArray[2, 0];//Will return either a boxState of "X" or "O"
        }
    
        return BoxState.Empty;//If nobody won, return an "Empty" boxState
    }
    public void UpdateGrid(int boxNum, BoxState state) //Used to update the grid based upon a simplified indentifier and a desired state
    {

        // 0 1 2
        // 3 4 5
        // 6 7 8

        //using remainder operator and divison to use simple ints to identify boxes
        //X Then Y
        //E.G 3%3=0 3/3=1 == grid coord [0,1]  8%8=2 8/3 = 2 grid coord == [2,2]

        if (gridArray[boxNum % 3, boxNum / 3] == BoxState.Empty)//If denoted grid coord is "empty"
        {
            gridArray[boxNum % 3, boxNum / 3] = state; //Change denoted grid coord to desiered state
        }
        
    }
   
}
