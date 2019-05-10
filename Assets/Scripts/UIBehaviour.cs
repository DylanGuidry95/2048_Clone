using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBehaviour : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject VictoryScreen;
    public UnityEngine.UI.Text ScoreDisplay;
    private void Awake()
    {
        Events.NoValidMovesRemain.AddListener(DisplayGameLost);
        Events.Victory.AddListener(DisplayVictory);
        Events.ScoreDisplay.AddListener(DisplayScore);
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

    void DisplayScore(int val)
    {
        ScoreDisplay.text = val.ToString();
    }
}
