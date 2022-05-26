using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityOnCollision : ScriptableObject
{
    public string Name;

    public virtual void Activate(GameObject parent) {}
    public virtual void Deactivate(GameObject parent) {}

}
