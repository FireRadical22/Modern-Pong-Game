using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCatalogue : MonoBehaviour
{
    [SerializeField]
    private List<Ability> catalogue = new List<Ability>();

    [SerializeField]
    private List<GameObject> affectedObjects = new List<GameObject>();

    [SerializeField]
    private List<Sprite> icons = new List<Sprite>();

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

}
