using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public float ballSpeedMultiplier;

    private GameObject ball;

    private float movement;

    public void Update()
    {
        if (isPlayer1) 
        {
            movement = Input.GetAxisRaw("Vertical");
        } 
        else 
        {
            movement = Input.GetAxisRaw("Vertical2");
        }
        
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
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
