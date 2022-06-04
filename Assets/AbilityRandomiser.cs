using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRandomiser : MonoBehaviour
{
    public GameObject box; //Pre fab
    // Todo:
    // Create a Sprite Renderer that spawns at random positions on map
    // Make it trigger --> Destroy block after trigger
    // Generates a random int that is stored in AbilityHolder
    
    public void Start()
    {
        // Initialise box at random position
    }
    
    
    public int GenNumber()
    {
        return (int) Random.Range(0,5);
    }

    public void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("Untagged"))
        {
            Destroy(box);
            // Check which player to assign to based on direction of ball
            // Add number to Ability Holder (check if ability holder is full or not)
        }
    }




}
