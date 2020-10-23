using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class Grid : MonoBehaviour
{
    public List <Button> boxes;

    public BoxState[,] gridArray = new BoxState[3, 3];

    // Start is called before the first frame update
    void Start()
    {
        ResetGrid();
    }
	public void ResetGrid()
	{
        
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gridArray[i, j] = BoxState.Empty;
                    
                }
            }
        for (int i = 0; i < 9; i++)
        {
            boxes[i].GetComponent<Box>().boxState = BoxState.Empty;
            boxes[i].GetComponent<Box>().boxText.text = "";
        }

        
    }
	// Update is called once per frame
	void Update()
    {
       
        
    }
    public BoxState checkWin()
    {
        for (int i = 0; i < 3; i++)
        {
            if (gridArray[i, 0] == gridArray[i, 1] && gridArray[i, 1] == gridArray[i, 2] && gridArray[i, 2] != BoxState.Empty)
            {
                return gridArray[i, 0];
            }
            if (gridArray[0, i] == gridArray[1, i] && gridArray[1, i] == gridArray[2, i] && gridArray[2, i] != BoxState.Empty)
            {
                return gridArray[0, i];
            }
        }
        if (gridArray[0, 0] == gridArray[1, 1] && gridArray[1, 1] == gridArray[2, 2] && gridArray[2, 2] != BoxState.Empty)
        {
            return gridArray[0, 0];
        }
        if (gridArray[2, 0] == gridArray[1, 1] && gridArray[1, 1] == gridArray[0, 2] && gridArray[0, 2] != BoxState.Empty)
        {
            return gridArray[2, 0];
        }
            //nobody won
            return BoxState.Empty;
        
    }
    public void UpdateGrid(int boxNum, BoxState state)
    {

        if (gridArray[boxNum % 3, boxNum / 3] == BoxState.Empty)
        {
            gridArray[boxNum % 3, boxNum / 3] = state;
        }
        
    }
   
}
