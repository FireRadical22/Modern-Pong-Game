using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public float ballSpeedMultiplier;

    public GameObject ObjectTracking;
    public static int difficulty;
    protected Vector2 ballPos;
    protected static float lowerBound = -3.5f;
    protected static float upperBound = 3.5f;

    delegate void Move();
    Move move;

    //protected virtual void Move() { }

    void Start()
    {
        startPosition = transform.position;
        switch(difficulty)
        {
            case 0:
                move = MediumMove;
                break;
            case 1:
                move = HardMove;
                break;
        }
    }

    void Update()
    {
        move();
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCollisionWithBall = collision.gameObject.CompareTag("Ball");

        if (isCollisionWithBall)
        {
            collision.GetComponent<Ball>().lastHitByPlayer1 = false;
            Ball ballScript = collision.GetComponent<Ball>();
            Rigidbody2D rb = ObjectTracking.GetComponent<Rigidbody2D>();
            if (ballScript.IsBelowSpeedLimit())
            {
                rb.velocity = new Vector2(rb.velocity.x * ballSpeedMultiplier, rb.velocity.y * ballSpeedMultiplier);
            }
            else
            {
                float XSpeedLimit = rb.velocity.x < 0 ? -10.0f : 10.0f;
                float YSpeedLimit = rb.velocity.y < 0 ? -10.0f : 10.0f;
                rb.velocity = new Vector2(XSpeedLimit, YSpeedLimit);
                ballScript.speedLimitReached = true;
            }
        }

    }

    void MediumMove()
    {
        ballPos = ObjectTracking.transform.localPosition;
        if (ObjectTracking.GetComponent<Ball>().lastHitByPlayer1)
        {
            if (transform.localPosition.y > ballPos.y && transform.localPosition.y > lowerBound)
            {
                transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
            }
            else if (transform.localPosition.y < ballPos.y && transform.localPosition.y < upperBound)
            {
                transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
            }
            else
            {
                transform.localPosition += Vector3.zero;
            }
        }
        else
        {
            transform.localPosition += Vector3.zero;
        }
    }

    void HardMove()
    {
        ballPos = ObjectTracking.transform.localPosition;
        if (transform.localPosition.y > ballPos.y && transform.localPosition.y > lowerBound)
        {
            transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
        }
        else if (transform.localPosition.y < ballPos.y && transform.localPosition.y < upperBound)
        {
            transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
        }
        else
        {
            transform.localPosition += Vector3.zero;
        }
    }
}
