using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject PauseMenuUI;
    public GameObject GameField;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ResumeGame();
            } else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        //GameField.SetActive(true);
        PauseMenuUI.SetActive(false);
        GameField.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void PauseGame()
    {
        //GameField.SetActive(false);
        PauseMenuUI.SetActive(true);
        GameField.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
