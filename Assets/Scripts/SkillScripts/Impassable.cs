using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Impassable : TimeAbility
{
    public float scaleFactor;

    public override void Activate(GameObject player) 
    {
        SoundManager.PlayImpassableSound();

        Vector3 modifiableScale = player.transform.localScale;
        modifiableScale.y *= scaleFactor;
        player.transform.localScale = modifiableScale;
    }

    public override void Deactivate(GameObject player) 
    {
        Vector3 modifiableScale = player.transform.localScale;
        modifiableScale.y /= scaleFactor;
        player.transform.localScale = modifiableScale;
    }
}
