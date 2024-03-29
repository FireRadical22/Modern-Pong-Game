using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu]
public class BounceBall : CollisionAbility
{
    public float gravity;

    public override void Activate(GameObject parent){
        SoundManager.PlayBounceBallSound();
        Rigidbody2D ball = parent.GetComponent<Rigidbody2D>();
        Vector2 v = ball.velocity;
        int negateY = v.y > 0 ? 1: -1;
        Launch(ball);
        ball.gravityScale = v.y > 0 ? gravity : -gravity;
    } 

    public override void Deactivate(GameObject parent){
        Rigidbody2D ball = parent.GetComponent<Rigidbody2D>();
        ball.gravityScale = 0;
        
    }

    private void Launch(Rigidbody2D body){
        body.velocity = new Vector2(-1 * body.velocity.x, body.velocity.y);
    }

}
