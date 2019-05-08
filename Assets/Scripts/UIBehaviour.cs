using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public GameObject GameOverScreen;

    private void Awake()
    {
        Events.NoValidMovesRemain.AddListener(DisplayGameLost);
    }    

    void DisplayGameLost()
    {
        GameOverScreen.SetActive(true);
    }

    public void RestartClicked()
    {
        Events.RestartGame.Invoke();
        GameOverScreen.SetActive(false);
    }
}
