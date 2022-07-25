using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Recall : TimeAbility
{

    public override void Activate(GameObject affectedObject)
    {
        affectedObject.GetComponent<Ball>().isRecalling = true;
    }

    public override void Deactivate(GameObject affectedObject)
    {
        affectedObject.GetComponent<Ball>().isRecalling = false;
    }



}
