using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlingshotAbility : TimeAbility
{
    public float velocityScale;

    public override void Activate(GameObject ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x * velocityScale, rb.velocity.y * velocityScale);
    }

    public override void Deactivate(GameObject ball)
    {
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x / velocityScale, rb.velocity.y / velocityScale);
    }
}
