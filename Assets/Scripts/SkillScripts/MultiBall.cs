using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MultiBall : CollisionAbility
{
    public int CloneCount;
    private Vector3 cloneSpreadVariance= new Vector3(0.5f, 0.5f, 0f);
    private GameObject[] clones;
    //private int trueBall;

    public override void Activate(GameObject PreFab)
    {
        SoundManager.PlayMultiBallSound();

        clones = new GameObject[CloneCount];
        int i = 0;
        
        while (i < CloneCount)
        {
            GameObject clone = Object.Instantiate(PreFab, PreFab.transform.position, Quaternion.identity); 
            clone.tag = "FakeBall";
            clone.layer = 6;
            clone.GetComponent<SpriteRenderer>().color = new Color(1.0f,1.0f, 1.0f, 0.9f);
            
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            cloneBody.velocity = new Vector2(
                PreFab.GetComponent<Ball>().getLastKnownVelocity().x * 1.1f,
                PreFab.GetComponent<Ball>().getLastKnownVelocity().y * 1.1f);

            cloneBody.velocity += new Vector2(
                Random.Range(-cloneSpreadVariance.x, cloneSpreadVariance.x),
                Random.Range(-cloneSpreadVariance.y, cloneSpreadVariance.y));

            /*cloneBody.velocity = new Vector2(
                -PreFab.GetComponent<Rigidbody2D>().velocity.x * 1.1f,
                PreFab.GetComponent<Rigidbody2D>().velocity.y * 1.1f);*/

            //cloneBody.velocity = direction;
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
