using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CurveBall : AbilityOnCollision
{
    public float gravity;

    public override void Activate(GameObject parent){
        Rigidbody2D ball = parent.GetComponent<Rigidbody2D>();
        Vector2 v = ball.velocity;
        int negateX = v.x > 0 ? 1: -1;

        
        v.x += gravity * Time.deltaTime * negateX;
        
    } 

    public override void Deactivate(GameObject parent){
        Rigidbody2D ball = parent.GetComponent<Rigidbody2D>();
        Vector2 v = ball.velocity;
        v.x += 0;
    }

}
