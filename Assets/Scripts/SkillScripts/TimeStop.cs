using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimeStop : TimeAbility
{

    Vector2 ballVelocity;
    float playerXPosition;

    private GameObject ball;

    // Ability User is passed in as affectedObject
    public override void Activate(GameObject affectedObject)
    {

        ball = affectedObject.GetComponent<AbilityHolder>().ball;

        // Save velocity before stopping time
        ballVelocity = ball.GetComponent<Rigidbody2D>().velocity;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        playerXPosition = affectedObject.GetComponent<Transform>().position.x;
        affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = true;
        Paddle.isTimeStopped = true;
    }

    public override void Deactivate(GameObject affectedObject)
    {
        // Set ball velocity back
        ball.GetComponent<Rigidbody2D>().velocity = ballVelocity;
        Paddle.isTimeStopped = false;
        affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = false;

        Vector3 pos = affectedObject.GetComponent<Transform>().position;

        //affectedObject.GetComponent<Transform>().position.Set(playerXPosition, pos.y, pos.z);
        affectedObject.transform.position = new Vector3(playerXPosition, affectedObject.transform.position.y, 0);
        affectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
