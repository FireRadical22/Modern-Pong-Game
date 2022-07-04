using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Slingshot : TimeAbility
{
    public float velocityScale;
    private static float speedLimitMultiplier = 1.1f;

    public override void Activate(GameObject ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if(ball.GetComponent<Ball>().speedLimitReached)
        {
            rb.velocity = new Vector2(rb.velocity.x * speedLimitMultiplier, rb.velocity.y * speedLimitMultiplier);
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x * velocityScale, rb.velocity.y * velocityScale);
        }
    }

    public override void Deactivate(GameObject ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (ball.GetComponent<Ball>().speedLimitReached)
        {
            rb.velocity = new Vector2(rb.velocity.x / speedLimitMultiplier, rb.velocity.y / speedLimitMultiplier);
        } else
        {
            rb.velocity = new Vector2(rb.velocity.x / velocityScale, rb.velocity.y / velocityScale);
        }
    }
}
