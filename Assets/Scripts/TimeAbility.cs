using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimeAbility : Ability
{
    public int activeTime;

    public float getActiveTime()
    {
        return activeTime;
    }
}
