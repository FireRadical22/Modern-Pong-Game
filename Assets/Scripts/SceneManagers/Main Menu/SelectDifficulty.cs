using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDifficulty : MonoBehaviour
{
    public void PlaySingle(int difficulty)
    {
        AIPaddle.difficulty = difficulty;
        SceneManager.LoadScene("SinglePlayer");
    }
}
