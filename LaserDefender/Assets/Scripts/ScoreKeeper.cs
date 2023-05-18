using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
 
    static ScoreKeeper instance; // Do not use in big projects

    private void Awake()
    {
        ManageSingleton();
    }


    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Dont destroy the gameobjet upon loading any other scene
        }
    }

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
