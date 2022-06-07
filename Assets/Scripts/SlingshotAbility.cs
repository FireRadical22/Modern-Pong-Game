using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SlingshotAbility : Ability
{
    public float velocityScale;
    

    public override void Activate(GameObject parent)
    {
        Rigidbody2D rigidBody = parent.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(rigidBody.velocity.x * velocityScale, rigidBody.velocity.y * velocityScale);
    }

    public override void Deactivate(GameObject parent)
    {
        Rigidbody2D rigidBody = parent.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(rigidBody.velocity.x / velocityScale, rigidBody.velocity.y / velocityScale);
    }
}
