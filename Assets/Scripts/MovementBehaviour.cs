using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{    
    private GridBehaviour GridRef;
    private void Awake() 
    {
        GridRef = FindObjectOfType(typeof(GridBehaviour)) as GridBehaviour;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {                           
            Move(new Vector3(0,-1,0));
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {            
            Move(new Vector3(0,1,0));
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {            
            Move(new Vector3(-1,0,0));
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {        
            Move(new Vector3(1,0,0));
        }
    }

    void Move(Vector3 direction)
    {
        foreach (var node in GridRef.Nodes)
        {
            if(GridRef.CheckBounds(node.transform.position + direction))
                node.transform.position += direction;
            GridRef.CheckSpaces();
        }
    }
}
