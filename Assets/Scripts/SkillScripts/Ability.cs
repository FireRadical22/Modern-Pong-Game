using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public new string name;

    public virtual void Activate(GameObject affectedObject) {}
    public virtual void Deactivate(GameObject affectedObject) {}
}
