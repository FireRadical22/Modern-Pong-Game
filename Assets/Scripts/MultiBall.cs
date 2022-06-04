using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MultiBall : AbilityOnCollision
{
    public int CloneCount;

    public override void Activate(GameObject PreFab)
    {
        float increment = 60 / CloneCount;
        float angle = -30;
        int i = 0;
        int realBall = (int) Random.Range(0, CloneCount);
        while (i <= CloneCount)
        {
            GameObject clone = Object.Instantiate(PreFab, PreFab.transform.position, Quaternion.identity); 
            if (i != realBall){
                clone.tag = "Untagged";
            }
            Rigidbody2D cloneBody = clone.GetComponent<Rigidbody2D>();
            //rotateVector(cloneBody.velocity, angle);
            Debug.Log("v.x: " + cloneBody.velocity.x + " v.y: " + cloneBody.velocity.y);
            Debug.Log("Angle: " + angle);
            angle = angle + increment;
            i++;
        }

        Deactivate(PreFab);
    }

    public override void Deactivate(GameObject affectedObject)
    {
        Destroy(affectedObject);
    }

    private void rotateVector(Vector2 vector, float angle)
    {
        vector.x = Mathf.Cos(angle) * vector.x - Mathf.Sin(angle) * vector.y;
        vector.y = Mathf.Sin(angle) * vector.x + Mathf.Cos(angle) * vector.y;
    }
}
