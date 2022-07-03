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

    public void Start()
    {
        startPosition = transform.position;
        Launch();
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
        return (rb.velocity.x < 7) && (rb.velocity.y < 7);  
    }


}
