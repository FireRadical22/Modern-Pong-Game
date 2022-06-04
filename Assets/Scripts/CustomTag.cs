using UnityEngine;
using System.Collections.Generic;
 

public class Pair<T, U> {
    public Pair() {
    }

    public Pair(T first, U second) {
        this.First = first;
        this.Second = second;
    }

    public T First { get; set; }
    public U Second { get; set; }
}


public class CustomTag : MonoBehaviour 
{
    //keys
    [SerializeField]
    private List<Ability> tags = new List<Ability>();
    
    [SerializeField]
    private List<GameObject> affectedObjects = new List<GameObject>();

     //create dictionary for keys and values (value is pair of ability and affected gameobject?)
    /*public bool HasTag(string tag)
    {
        return tags.Contains(tag);
    }
     
    public IEnumerable<string> GetTags()
    {
        return tags;
    }
     
    public void Rename(int index, string tagName)
    {
        tags[index] = tagName;
    }
     
    public string GetAtIndex(int index)
    {
        return tags[index];
    }
     
    public int Count
    {
        get { return tags.Count; }
    }*/
}
