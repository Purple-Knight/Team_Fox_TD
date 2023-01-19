using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return instance; } }
    private static GameManager instance;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            Destroy(this);
            return;
        }
        else 
        {
            instance = this;
        }
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync("GameOverScene", LoadSceneMode.Additive);
    }
}
