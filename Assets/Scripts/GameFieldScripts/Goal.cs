using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;
    public GameObject manager;
    public GameObject GoalAnimation;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Ball")) 
        {
            if (!isPlayer1Goal) 
            {
                Debug.Log("Player 1 Scored!");
                manager.GetComponent<GameManager>().Player1Scored();
                foreach (Transform child in GoalAnimation.transform) 
                {
                    child.GetComponent<ParticleSystem>().Play();
                }
            } 
            else 
            {
                Debug.Log("Player 2 Scored!");
                manager.GetComponent<GameManager>().Player2Scored();
                foreach (Transform child in GoalAnimation.transform)
                {
                    child.gameObject.GetComponent<ParticleSystem>().Play();
                }
            }
        }
    }

}

