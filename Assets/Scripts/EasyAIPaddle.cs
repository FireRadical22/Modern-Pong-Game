using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAIPaddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    // Update is called once per frame
    void Update()
    {
        //AI Movement
        GameObject ball = GameObject.Find("Ball");
        float yPaddle = rb.position.y;
        float distance = ball.transform.position.y - yPaddle;
        if (distance > 5.0)
        {
            while (Mathf.Abs(yPaddle - ball.transform.position.y) > 2.0)
            {
                rb.velocity = new Vector2(rb.velocity.x, speed);
            }
        } else if (distance < -5.0)
        {
            while (Mathf.Abs(yPaddle- ball.transform.position.y) < -2.0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -1 * speed);
            }
        } else 
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        
    }

    void Start() 
    {
        startPosition = transform.position;
    }

    public void Reset() 
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
