using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Ability ability;
    public KeyCode key;
    public GameObject player;

    private States GameState = States.inactive;
    private float timer;

    enum States{
        active,
        inactive,
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameState)
        {
            case States.active:
                timer -= Time.deltaTime;
                if(Input.GetKeyDown(key) || timer < 0)
                {
                    GameState = States.inactive;
                    ability.Deactivate(player);
                }
                break;
            case States.inactive:
                if(Input.GetKeyDown(key))
                {
                    GameState = States.active;
                    ability.Activate(player);
                    timer = ability.activeTime;
                }
                break;
        }
    }

    public void ResetAllAbilities(){
        if (GameState == States.active){
            GameState = States.inactive;
            ability.Deactivate(player);
        }
    }
}
