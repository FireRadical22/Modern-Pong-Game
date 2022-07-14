using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1BallDetector : MonoBehaviour
{
    public GameObject player1;
    public BounceBall abilitytodetect;

    private GameObject BallAI;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (player1.GetComponent<AbilityHolder>().currentAbilityInUse is BounceBall && collision.gameObject.CompareTag("BallAI"))
        {
            BallAI = collision.gameObject;
            BallAI.GetComponent<Rigidbody2D>().gravityScale = abilitytodetect.gravity;
        }
    }

    public void Reset()
    {
        BallAI.GetComponent<Rigidbody2D>().gravityScale = 0;    
    }
}
