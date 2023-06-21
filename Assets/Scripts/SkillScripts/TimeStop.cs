using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimeStop : TimeAbility
{

    public static Vector2 resultantVelocity;
    float playerXPosition;
    float playerYPosition;

    private GameObject ball;

    // Ability User is passed in as affectedObject
    public override void Activate(GameObject affectedObject)
    {
        playerXPosition = affectedObject.GetComponent<Transform>().position.x;
        playerYPosition = affectedObject.GetComponent<Transform>().position.y;

        SoundManager.PlayTimeStopSound();
        ball = affectedObject.GetComponent<AbilityHolder>().ball;

        // Save velocity before stopping time
        resultantVelocity = ball.GetComponent<Rigidbody2D>().velocity;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = true;
        Paddle.isTimeStopped = true;
    }

    public override void Deactivate(GameObject affectedObject)
    {
        // Set ball velocity back
        ball.GetComponent<Rigidbody2D>().velocity = resultantVelocity;
        Paddle.isTimeStopped = false;
        affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = false;

        Vector3 pos = affectedObject.GetComponent<Transform>().position;

        affectedObject.transform.position = new Vector3(playerXPosition, playerYPosition, 0);
        affectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

}
