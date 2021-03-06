using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InvisBall : CollisionAbility
{
    public override void Activate(GameObject parent){
        SoundManager.PlayInvisBallSound();
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        parent.GetComponent<TrailRenderer>().enabled = false;

    }

    public override void Deactivate(GameObject parent){
        SpriteRenderer sprite = parent.GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        parent.GetComponent<TrailRenderer>().enabled = true;
    }
}
