using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InvisBall : AbilityOnCollision
{
    public override void Activate(GameObject parent){
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    public override void Deactivate(GameObject parent){
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        sprite.enabled = true;
    }
}
