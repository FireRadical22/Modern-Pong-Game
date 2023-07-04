using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1BallDetector : MonoBehaviour
{
    public GameObject player1;
    //public BounceBall abilitytodetect;

    public GameObject BallAI;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BallAI"))
        {
            Reset();
            if (player1.GetComponent<AbilityHolder>().currentAbilityInUse is BounceBall)
            {
                BallAI.GetComponent<InvisibleBallAI>().DeactivatePreFab();
            }
            //BallAI = collision.gameObject;
            //Reset();
            //if (player1.GetComponent<AbilityHolder>().currentAbilityInUse is BounceBall)
            //{
            //    BallAI.GetComponent<InvisibleBallAI>().DeactivatePreFab();
            //    //player1.GetComponent<AbilityHolder>().currentAbilityInUse.Activate(BallAI);
            //}
        }
    }

    public void Reset()
    {
        BallAI.GetComponent<Rigidbody2D>().gravityScale = 0;    
    }
}
