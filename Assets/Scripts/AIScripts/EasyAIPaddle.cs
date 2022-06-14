using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAIPaddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    public GameObject ObjectTracking;
    private Vector2 ballPos;

    // Update is called once per frame
    void Update()
    {
        Move();

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

    private void Move()
    {
        ballPos = ObjectTracking.transform.localPosition;
        if (transform.localPosition.y > ballPos.y)
        {
            transform.localPosition += new Vector3(0, -speed * Time.deltaTime, 0);
        } else if (transform.localPosition.y < ballPos.y)
        {
            transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);
        } else 
        {
            transform.localPosition += Vector3.zero;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        bool isCollisionWithBall = collision.gameObject.CompareTag("Ball");

        if (isCollisionWithBall)
        {
            collision.GetComponent<Ball>().lastHitByPlayer1 = false;
        }

    }
}

