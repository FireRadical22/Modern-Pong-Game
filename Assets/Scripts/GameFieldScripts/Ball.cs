using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public bool isModifiedByPlayer1;
    public bool isModifiedByPlayer2;
    public bool lastHitByPlayer1;

    public bool speedLimitReached;
    private Vector3 finalPosition;

    public void Start()
    {
        startPosition = transform.position;
        Launch();
    }

    public void Update()
    {
        if (!IsBelowSpeedLimit() && rb.gravityScale == 0)
        {
            float XSpeedLimit = rb.velocity.x < 0 ? -10.0f : 10.0f;
            float YSpeedLimit = rb.velocity.y < 0 ? -10.0f : 10.0f;
            rb.velocity = new Vector2(XSpeedLimit, YSpeedLimit);
            speedLimitReached = true;
        }
    }

    public void Reset() 
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        gameObject.GetComponent<TrailRenderer>().Clear();
        Launch();
    }

    private void Launch() 
    {
        float x = Random.Range(0,2) == 0 ? -1 : 1;
        float y = Random.Range(0,2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    public bool IsBelowSpeedLimit()
    {
        return (Mathf.Abs(rb.velocity.x) < 10) && (Mathf.Abs(rb.velocity.y) < 10);  
    }

    public Vector3 GetFinalPosition()
    {
        if (transform.localPosition.x == 18.0f && gameObject.CompareTag("BallAI"))
        {
            finalPosition = transform.localPosition;
            Destroy(gameObject);
        }
        else
        {
            finalPosition = Vector3.zero;
        }

        return finalPosition;
    }
}
