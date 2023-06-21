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
    public bool isRecalling;

    public LinkedList<Vector2> list;
    public LinkedList<Vector2> velocityList;

    public bool speedLimitReached;
    private Vector3 finalPosition;



    public void Start()
    {
        startPosition = transform.position;
        Launch();
        list = new LinkedList<Vector2>();
        velocityList = new LinkedList<Vector2>();
    }

    public void Update()
    {
        if (!IsBelowSpeedLimit() && rb.gravityScale == 0)
        {
            float XSpeedLimit = rb.velocity.x < 0 ? -15.0f : 15.0f;
            float YSpeedLimit = rb.velocity.y < 0 ? -15.0f : 15.0f;
            rb.velocity = new Vector2(XSpeedLimit, YSpeedLimit);
            speedLimitReached = true;
        }

        if (Paddle.isTimeStopped)
        {
            float newX = rb.velocity.x / 1.003f; // decay factor
            float newY = rb.velocity.y / 1.003f;
            rb.velocity = new Vector2(newX, newY);
        }
    }

    public void FixedUpdate()
    {
        if (isRecalling)
        {
            if (list.First != null)
            {
                rb.MovePosition(list.First.Value);
                list.RemoveFirst();
            }
            else
            {
                rb.velocity = velocityList.Last.Value;
                velocityList = new LinkedList<Vector2>();
                isRecalling = false;
            }
        }
        else
        {
            list.AddLast(rb.position);
            velocityList.AddLast(rb.velocity);

            if (list.Count > 300)
            {
                list.RemoveFirst();
                velocityList.RemoveFirst();
            }
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
        return (Mathf.Abs(rb.velocity.x) < 15) && (Mathf.Abs(rb.velocity.y) < 15);  
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (Paddle.isTimeStopped)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                TimeStop.resultantVelocity += rb.velocity;
            }
        }

    }

}
