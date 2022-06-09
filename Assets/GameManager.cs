using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private int Player1Score;
    private int Player2Score;

    public void Player1Scored() 
    {
        Player1Score++;
        Player1Text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        Player1Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
        if (Player2Paddle.GetComponent<AbilityHolder>().isActiveAndEnabled){
            Player2Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
        } else 
        {
            Player2Paddle.GetComponent<AIAbilityHolder>().ResetAllAbilities();
        }
        ResetPosition();
    }

    public void Player2Scored() 
    {
        Player2Score++;
        Player2Text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        Player1Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
        if (Player2Paddle.GetComponent<AbilityHolder>().isActiveAndEnabled == true){
            Player2Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
        } else 
        {
            Player2Paddle.GetComponent<AIAbilityHolder>().ResetAllAbilities();
        }
        ResetPosition();
    }

    private void ResetPosition()
    {
        Ball.GetComponent<Ball>().Reset();
        Ball.GetComponent<SpriteRenderer>().enabled = true;
        Player1Paddle.GetComponent<Paddle>().Reset();
        if (Player2Paddle.GetComponent<Paddle>().isActiveAndEnabled == true)
        {
            Player2Paddle.GetComponent<Paddle>().Reset();
        } else 
        {
            Player2Paddle.GetComponent<EasyAIPaddle>().Reset();
        }
    }

    //private void isSinglePlayer()
    //{
    //    Player1Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
    //    if (Player2Paddle.GetComponent<AbilityHolder>().isActiveAndEnabled){
    //        Player2Paddle.GetComponent<AbilityHolder>().ResetAllAbilities();
    //    } else 
    //    {
    //        Player2Paddle.GetComponent<AIAbilityHolder>().ResetAllAbilities();
    //    }
    //}
    
}
