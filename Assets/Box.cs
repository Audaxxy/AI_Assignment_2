//100653593	
//Nathan Boldy
//10/23/20

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; //Including for usage of the Text box

//Script responsible with storing the basics of each "box" of the grid
public enum BoxState //Stores state of boxes as either empty, O, or X for ease of reading
{
    Empty, O, X
}


public class Box : MonoBehaviour
{
    
    public BoxState boxState; // Creating a publicly accessible boxState

    public int boxIdentifier;  // A value from 0-8 assigned in Unity editor for easier indentification of boxes

    public Text boxText;  // The text box component of the Unity buttons

    void Start() // Start is called before the first frame update
    {
         boxText = GetComponentInChildren<Text>(); //Storing reference to text box
    }
}
