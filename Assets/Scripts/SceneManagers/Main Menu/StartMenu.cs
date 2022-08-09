using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SelectMode");
    }

    public void Secret()
    {
        SceneManager.LoadScene("Secret");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
