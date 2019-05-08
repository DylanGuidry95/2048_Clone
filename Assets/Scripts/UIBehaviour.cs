using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;

    private void Awake()
    {
        Events.NoValidMovesRemain.AddListener(DisplayGameLost);
        Events.Victory.AddListener(DisplayVictory);
    }    

    void DisplayGameLost()
    {
        GameOverScreen.SetActive(true);
    }

    public void RestartClicked()
    {
        Events.RestartGame.Invoke();
        GameOverScreen.SetActive(false);
        VictoryScreen.SetActive(false);
    }

    void DisplayVictory()
    {
        VictoryScreen.SetActive(true);
    }
}
