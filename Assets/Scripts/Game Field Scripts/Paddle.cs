using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

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
            collision.GetComponent<Ball>().lastHitByPlayer1 = isPlayer1;
        }

    }
}
