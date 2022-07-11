using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static bool isTimeStopped = false;
    public bool canMoveDuringTimeStop = false;

    public bool isPlayer1;
    //public bool isSinglePlayer;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public float ballSpeedMultiplier;

    private GameObject ball;

    private float movementX;
    private float movementY;

    public void Update()
    {
        if (isTimeStopped)
        {
            if (isPlayer1)
            {
                if (canMoveDuringTimeStop)
                {
                    movementY = Input.GetAxisRaw("Vertical");
                    movementX = Input.GetAxisRaw("Horizontal");
                    rb.velocity = new Vector2(movementX * speed, movementY * speed);
                }
            }
            else
            {
                if (canMoveDuringTimeStop)
                {
                    movementY = Input.GetAxisRaw("Vertical2");
                    movementX = Input.GetAxisRaw("Horizontal2");
                    rb.velocity = new Vector2(movementX * speed, movementY * speed);
                }
            }
        }
        else
        {
            if (isPlayer1)
            {
                movementY = Input.GetAxisRaw("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, movementY * speed);
            }
            else
            {
                movementY = Input.GetAxisRaw("Vertical2");
                rb.velocity = new Vector2(rb.velocity.x, movementY * speed);
            }
        }

    }

    public void Start() 
    {
        startPosition = transform.position;
        ball = GameObject.FindGameObjectWithTag("Ball");
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
            Ball ballScript = collision.GetComponent<Ball>();
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

            ballScript.lastHitByPlayer1 = isPlayer1;

            /*if (isSinglePlayer && AIPaddle.difficulty == 2)
            {
                GameObject clone = Object.Instantiate(ball);
                //clone.GetComponent<SpriteRenderer>().enabled = true;
                clone.GetComponent<TrailRenderer>().enabled = false;
                Rigidbody2D cloneRB = clone.GetComponent<Rigidbody2D>();
                cloneRB.velocity = new Vector2(rb.velocity.x * 2.0f, rb.velocity.y * 2.0f);
                clone.tag = "BallAI";
                clone.layer = 6;
            }*/

            if (ballScript.IsBelowSpeedLimit())
            {
                rb.velocity = new Vector2(rb.velocity.x * ballSpeedMultiplier, rb.velocity.y * ballSpeedMultiplier);
            } else
            {
                float XSpeedLimit = rb.velocity.x < 0 ? -10.0f : 10.0f;
                float YSpeedLimit = rb.velocity.y < 0 ? -10.0f : 10.0f;
                rb.velocity = new Vector2(XSpeedLimit, YSpeedLimit);
                ballScript.speedLimitReached = true;
            }
        }
    }
}
