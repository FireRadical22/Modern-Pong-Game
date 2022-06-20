using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject Ball;

    [Header("Player 1")]
    public GameObject Player1Paddle;
    public GameObject Player1Goal;

    [Header("Player 2")]
    public GameObject Player2Paddle;
    public GameObject Player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;

    [Header("Game Over Scene")]
    public Scene GameOverScene;

    public bool isSingleplayer;
    private int Player1Score;
    private int Player2Score;

    
    public void Player1Scored() 
    {
        Player1Score++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        if (Player1Score == 1)
        {
            Winner(1);
        } else
        {
            Player1Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
            if (isSingleplayer)
            {
                Player2Paddle.GetComponent<AIAbilityHolder>().ResetAllAbilities();
            }
            else
            {
                Player2Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
            }
            ResetPosition();
        }
    }

    public void Player2Scored() 
    {
        Player2Score++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        if (Player2Score == 1)
        {
            Winner(2);
        } else
        {
            Player1Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
            if (isSingleplayer)
            {
                Player2Paddle.GetComponent<AIAbilityHolder>().ResetAllAbilities();
            }
            else
            {
                Player2Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
            }
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        Ball.GetComponent<Ball>().Reset();
        Ball.GetComponent<SpriteRenderer>().enabled = true;
        Player1Paddle.GetComponent<Paddle>().Reset();
        if (isSingleplayer)
        {
            Player2Paddle.GetComponent<EasyAIPaddle>().Reset();
        } 
        else 
        {
            Player2Paddle.GetComponent<Paddle>().Reset();
        }
    }

    private void Winner(int winner)
    {
        SceneManager.LoadScene("GameOver");
        if (isSingleplayer)
        {
            if (winner == 1)
            {
                StateNameController.heading = "Victory!!!";
                StateNameController.winner = "You Win!";
            } else
            {
                StateNameController.heading = "Game Over";
                StateNameController.winner = "You Lose...";
            }
        } else
        {
            StateNameController.heading = "Victory!!!";
            string result = "";
            result = "Player " + winner.ToString() + " wins!";
            StateNameController.winner = result;
        }
        
    }
    
}
