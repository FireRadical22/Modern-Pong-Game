using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAIDetector : MonoBehaviour
{
    private Vector3 FinalDest;
    private bool BallHasReached;

    public void Start()
    {
        BallHasReached = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BallAI") && collision.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            BallHasReached = true;
            FinalDest = collision.transform.position;
            collision.gameObject.GetComponent<InvisibleBallAI>().DeactivatePreFab();
        }
    }
    public Vector3 GetFinalDest()
    {
        //BallHasReached = false;
        return FinalDest;
    }

    public bool HasReached()
    {
        return BallHasReached;
    }

    public void Reset()
    {
        BallHasReached = false;
    }
}
