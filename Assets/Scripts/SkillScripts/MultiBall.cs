using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MultiBall : CollisionAbility
{
    public int CloneCount;
    private GameObject[] clones;

    public override void Activate(GameObject PreFab)
    {
        clones = new GameObject[CloneCount];
        int i = 0;
        
        while (i < CloneCount)
        {
            GameObject clone = Object.Instantiate(PreFab, PreFab.transform.position, Quaternion.identity); 
            clone.tag = "Untagged";
            clone.GetComponent<SpriteRenderer>().color = Color.gray;
            
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            
            cloneBody.AddForce(new Vector2(0, (i+1) * 50), ForceMode2D.Force);
            clones[i] = clone;
            i++;
        }

        
    }

    public override void Deactivate(GameObject affectedObject)
    {
        for (int j = 0; j < CloneCount; j++)
        {   
            Destroy(clones[j]);
        }
    }
}
