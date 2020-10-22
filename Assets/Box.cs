using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BoxState
{
    Empty, O, X
}


public class Box : MonoBehaviour
{
    public BoxState boxState;
    // 0-8
    public int boxIdentifier;
    public Text boxText;
    // Start is called before the first frame update
    void Start()
    {
        boxText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
