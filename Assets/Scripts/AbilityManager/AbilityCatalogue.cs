using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCatalogue : MonoBehaviour
{
    [SerializeField]
    public List<Ability> catalogue = new();

    [SerializeField]
    public List<GameObject> affectedObjects = new();

    [SerializeField]
    public List<Sprite> icons = new();

    [SerializeField]
    public List<string> abilityDescriptions = new();


    public Sprite GetIcon(int i)
    {
        return icons[i];
    }

    public GameObject GetAffectedObject(int i)
    {
        return affectedObjects[i];
    }

    public Ability GetAbility(int i)
    {
        return catalogue[i];
    }

    public string GetDescription(int i)
    {
        return abilityDescriptions[i];
    }

}
