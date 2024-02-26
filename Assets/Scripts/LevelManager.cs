using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float GameOverLoadDelay = 1.5f;
    ScoreKeeeper scoreKeeeper;

    private void Awake()
    {
        scoreKeeeper = FindObjectOfType<ScoreKeeeper>();
    }
    public void LoadGame()
    {
        scoreKeeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Game Over", GameOverLoadDelay));
    }

    public void QuitGame()
    {
        Debug.Log("Quiting the game");
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadScene(sceneName);
    }
}
