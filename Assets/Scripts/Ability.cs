using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float activeTime;

    public virtual void Activate(GameObject player) {}
    public virtual void Deactivate(GameObject player) {}
}
