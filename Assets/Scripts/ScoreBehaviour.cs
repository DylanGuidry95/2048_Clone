using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    public int CurrentScore;

    private void Awake()
    {
        CurrentScore = 0;
        Events.ScoreUpdate.AddListener(UpdateScore);
    }

    void UpdateScore(int value)
    {
        CurrentScore += value;
    }
}
