using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{        
    private void Awake() 
    {
             
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {                           
            Move(new Vector3(0,-2,0));
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {            
            Move(new Vector3(0,2,0));
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {            
            Move(new Vector3(-2,0,0));
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {        
            Move(new Vector3(2,0,0));
        }
    }

    void Move(Vector3 direction)
    {
        
    }
}
