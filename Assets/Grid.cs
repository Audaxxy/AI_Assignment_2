using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class Grid : MonoBehaviour
{
    public Button box0, box1, box2, box3, box4, box5, box6, box7, box8;

    public BoxState[,] gridArray = new BoxState[3, 3];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                gridArray[i,j] = BoxState.Empty;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }

    public void UpdateGrid(int boxNum, BoxState state)
    {

        if (gridArray[boxNum / 3, boxNum % 3] == BoxState.Empty)
        {
            gridArray[boxNum / 3, boxNum % 3] = state;
        }
        
    }
   
}
