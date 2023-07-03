using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBallAI : MonoBehaviour
{
    public float SpeedMultiplier;
    public Vector3 startPos;

    private static float maxBallSpeed = 20.0f;

    //private Vector3 finalPos;
    //private bool ballReached;
    // Start is called before the first frame update
    void Start()
    {
        //ballReached = false;
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        //finalPos = Vector3.zero;
    }

    public void ActivatePreFab(GameObject affectedObject)
    {
        //launches tracking ball at linear velocity
        transform.localPosition = affectedObject.transform.localPosition;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Vector2 rbvelocity = affectedObject.GetComponent<Rigidbody2D>().velocity;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
            Mathf.Max(-rbvelocity.x * SpeedMultiplier, -maxBallSpeed), 
            Mathf.Min(rbvelocity.y * SpeedMultiplier, maxBallSpeed));
    }

    public void DeactivatePreFab()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = startPos;
    }
}
