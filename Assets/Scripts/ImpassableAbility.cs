using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ImpassableAbility : Ability
{
    public float scaleFactor;
    
    public override void Activate(GameObject player) 
    {
        player.transform.localScale += new Vector3(0f, scaleFactor, 0f);
    }

    public override void Deactivate(GameObject player) 
    {
        player.transform.localScale += new Vector3(0f, -scaleFactor, 0f);
    }
}
