using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static bool isTimeStoppedByPlayer1 = false;
    public static bool isTimeStoppedByPlayer2 = false;
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
        if (Paddle.isTimeStoppedByPlayer1 || Paddle.isTimeStoppedByPlayer2)
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
            Debug.Log(ballScript.rb.velocity.x);
            Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

            ballScript.lastHitByPlayer1 = isPlayer1;

            if (ballScript.IsBelowSpeedLimit())
            {
                //Debug.Log(rb.velocity.y);
                rb.velocity = new Vector2(rb.velocity.x * ballSpeedMultiplier, rb.velocity.y * ballSpeedMultiplier);
                //Debug.Log(rb.velocity.y);
            } else
            {
                float XSpeedLimit = rb.velocity.x < 0 ? -15.0f : 15.0f;
                float YSpeedLimit = rb.velocity.y < 0 ? -15.0f : 15.0f;
                rb.velocity = new Vector2(XSpeedLimit, YSpeedLimit);
                ballScript.speedLimitReached = true;
            }
        }
    }
}
