using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TimeStop : TimeAbility
{

    public static Vector2 resultantVelocity = Vector2.zero;
    private static GameObject Player1;
    private static GameObject Player2;
    private GameObject ball;
    private bool isFirstActivation = true;

    float player1XPosition;
    float player1YPosition;

    float player2XPosition;
    float player2YPosition;

    // Ability User is passed in as affectedObject
    public override void Activate(GameObject affectedObject)
    {
        if (isFirstActivation)
        {
            isFirstActivation = false;
            ball = GameObject.Find("Ball");
        }

        SoundManager.PlayTimeStopSound();
        if (affectedObject.name == "Player1")
        {
            player1XPosition = affectedObject.GetComponent<Transform>().position.x;
            player1YPosition = affectedObject.GetComponent<Transform>().position.y;
            ball = affectedObject.GetComponent<AbilityHolder>().ball;

            // Save velocity before stopping time
            resultantVelocity += ball.GetComponent<Rigidbody2D>().velocity;
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = true;
            Paddle.isTimeStoppedByPlayer1 = true;
        }

        if (affectedObject.name == "Player2")
        {
            player2XPosition = affectedObject.GetComponent<Transform>().position.x;
            player2YPosition = affectedObject.GetComponent<Transform>().position.y;
            ball = affectedObject.GetComponent<AbilityHolder>().ball;

            // Save velocity before stopping time
            resultantVelocity += ball.GetComponent<Rigidbody2D>().velocity;
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = true;
            Paddle.isTimeStoppedByPlayer2 = true;
        }
    }

    public override void Deactivate(GameObject affectedObject)
    {
        if (affectedObject.name == "Player1")
        {
            Paddle.isTimeStoppedByPlayer1 = false;
            affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = false;
            affectedObject.transform.position = new Vector3(player1XPosition, player1YPosition, 0);
            affectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (affectedObject.name == "Player2")
        {
            Paddle.isTimeStoppedByPlayer2 = false;
            affectedObject.GetComponent<Paddle>().canMoveDuringTimeStop = false;
            affectedObject.transform.position = new Vector3(player2XPosition, player2YPosition, 0);
            affectedObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        if (!Paddle.isTimeStoppedByPlayer1 && !Paddle.isTimeStoppedByPlayer2)
        {
            // Set ball velocity back
            ball.GetComponent<Rigidbody2D>().velocity = resultantVelocity;
        }
    }

}
