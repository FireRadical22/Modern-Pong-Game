using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAIPaddle : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;
    public GameObject ObjectTracking;

    // Update is called once per frame
    void Update()
    {
        //AI Movement
        Rigidbody2D paddleBody = gameObject.GetComponent<Rigidbody2D>();
        if (paddleBody.position.y < ObjectTracking.transform.position.y){
            paddleBody.velocity = new Vector2()
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
