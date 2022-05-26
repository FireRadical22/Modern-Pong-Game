using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAbility : MonoBehaviour
{
    public AbilityOnCollision ability;
    public KeyCode key;
    public GameObject player;

    private States GameState = States.inactive;

     enum States{
        activated,
        active,
        inactive,
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState == States.inactive && Input.GetKeyDown(key)){
            GameState = States.active;
        }
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.CompareTag("Ball") && GameState == States.active){
            GameState = States.activated;
            ability.Activate(player);
        } else if (collider.gameObject.GetComponent<CustomTag>().HasTag("CollisionWallTag") && GameState == States.activated){
            GameState = States.inactive;
            ability.Deactivate(player);
        }
    }

    /*void OnTriggerExit2D(Collider2D collider){
        if (collider.gameObject.CompareTag("CollisionWallTag") && GameState == States.activated){
            GameState = States.inactive;
            ability.Deactivate(player);
        }
    }*/
}
