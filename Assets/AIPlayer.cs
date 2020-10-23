using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIPlayer : PlayerBase
{
    Vector2Int bestBox;
    int bestResult;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameHandler.currentPlayer != this||GameHandler.gameOver)
        {
            return;
        }
        
        bestBox=Vector2Int.zero;
        bestResult = -2;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid.gridArray[i, j] != BoxState.Empty)
                {
                    continue;
                }

                grid.gridArray[i, j] = (mySymbol == MySymbol.X) ? BoxState.X : BoxState.O;
                int result = Move((mySymbol == MySymbol.X) ? BoxState.O : BoxState.X,true);
                if (result > bestResult)
                {
                    bestResult = result;
                    bestBox = new Vector2Int(i, j);
                    
                }
                grid.gridArray[i, j] = BoxState.Empty;
                //Debug.Log(result);
            }
        }
        grid.gridArray[bestBox.x, bestBox.y] = (mySymbol == MySymbol.X) ? BoxState.X : BoxState.O;
        grid.boxes[bestBox.x + bestBox.y * 3].GetComponent<Box>().boxText.text= mySymbol.ToString();
        grid.boxes[bestBox.x + bestBox.y * 3].GetComponent<Box>().boxState = grid.gridArray[bestBox.x, bestBox.y];
        gameHandler.nextTurn();
    }
	public int Move(BoxState symbol,bool isRoot)
	{
        int moveNum = 0;
        //if symbol is the same as my symbol, set inital best move to -2, otherwise set to 2
        int bestMove = ((symbol == BoxState.X) == (mySymbol == MySymbol.X)) ? -2 : 2;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (grid.gridArray[i, j] != BoxState.Empty)
                {
                    continue;
                }
                moveNum++;
                int result=0;
                grid.gridArray[i, j] = symbol;
                
                BoxState winner = grid.checkWin();
                if (winner == BoxState.X)
                {
                    if (mySymbol == MySymbol.X)
                    {
                        result = 1;
                    }
                    else if (mySymbol == MySymbol.O)
                    {
                        result = -1;
                    }
                }

                else if (winner == BoxState.O)
                {
                    if (mySymbol == MySymbol.O)
                    {
                        result = 1;
                    }
                    else if (mySymbol == MySymbol.X)
                    {
                        result = -1;
                    }
                }
                else
                {
                    //Ternary operator, swaps current state for next recursion
                    result = Move((symbol == BoxState.X )? BoxState.O:BoxState.X,false) ;
                }
                grid.gridArray[i, j] = BoxState.Empty;
                
                if (symbol == BoxState.X)
                {
                    if (mySymbol == MySymbol.X)
                    {
                        bestMove = Mathf.Max(bestMove, result);
                    }
                    else if (mySymbol == MySymbol.O)
                    {
                        bestMove = Mathf.Min(bestMove, result);
                    }
                }
                if (symbol == BoxState.O)
                {
                    if (mySymbol == MySymbol.O)
                    {
                        bestMove = Mathf.Max(bestMove, result);
                    }
                    else if (mySymbol == MySymbol.X)
                    {
                        bestMove = Mathf.Min(bestMove, result);
                    }
                }


            }
        }
        if (moveNum == 0)
        {
            return 0;
        }
        else
        {
            return bestMove;
        }
        
       


    }

}
