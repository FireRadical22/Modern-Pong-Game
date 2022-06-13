using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMode : MonoBehaviour
{
    public void PlayPvP()
    {
        SceneManager.LoadScene("NormalPVP");
    }

    public void PlaySingle()
    {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
