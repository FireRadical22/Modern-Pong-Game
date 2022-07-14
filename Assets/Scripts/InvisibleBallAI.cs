using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleBallAI : MonoBehaviour
{
    public float SpeedMultiplier;
    public Vector3 startPos;

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
        transform.localPosition = affectedObject.transform.localPosition;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Vector2 rbvelocity = affectedObject.GetComponent<Rigidbody2D>().velocity;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-rbvelocity.x * SpeedMultiplier, rbvelocity.y * SpeedMultiplier);
    }

    public void DeactivatePreFab()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = startPos;
    }
}
