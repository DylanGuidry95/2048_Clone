using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeBehaviour))]
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
            Slide(new Vector3(0,-2,0));
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Slide(new Vector3(0,2,0));
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Slide(new Vector3(-2,0,0));
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Slide(new Vector3(2,0,0));
        }
    }

    void Slide(Vector3 direction)
    {
        while(GridRef.CheckVacancy(transform.position + direction))
        {
            transform.position += direction;
        }
        GridRef.CheckSpaces();
    }
}
