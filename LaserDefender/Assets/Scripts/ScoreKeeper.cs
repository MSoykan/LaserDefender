using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
 
    public void modifyScore(int points)
    {
        score += points;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log("My score is "+ score);
    }

    public void resetScore()
    {
        score = 0;
    }

    public int GetScore()
    {
        return score;
    }
}
