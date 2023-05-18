using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    [SerializeField] float sceneLoadDelay = 2f;
    [SerializeField] ScoreKeeper scoreKeeper;


    private void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadGame()
    {
        if (scoreKeeper != null)
        {
            scoreKeeper.resetScore();
        }
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGameOverMenu()
    {
        StartCoroutine(WaitAndLoad("GameOver", sceneLoadDelay)); ;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the Game!");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
