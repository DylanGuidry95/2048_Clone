using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    private void Awake()
    {
        Events.NoValidMovesRemain.AddListener(DisplayGameLost);
    }    

    void DisplayGameLost()
    {
        Debug.Log("YOU LOST");
    }
}
