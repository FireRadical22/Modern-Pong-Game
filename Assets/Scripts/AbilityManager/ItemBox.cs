using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject catalogue;
    public GameObject itemBox;
    public float cooldown;
    public float despawnTime;
    public bool isSingleplayer;

    private int numberOfAbilities;
    private float timer;
    private bool itemBoxIsActive;
    private float uptime;

    public void Start()
    {
        SetItemBoxActiveState(false);
        timer = cooldown;
        RandomisePosition();
        numberOfAbilities = catalogue.GetComponent<AbilityCatalogue>().catalogue.Count;
    }

    public void Update()
    {
        if (!Paddle.isTimeStoppedByPlayer1 && !Paddle.isTimeStoppedByPlayer2)
        {
            if (!itemBoxIsActive)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    SetItemBoxActiveState(true);
                    uptime = despawnTime;
                    RandomisePosition();
                }
            }
            else
            {
                uptime -= Time.deltaTime;

                if (uptime <= 0)
                {
                    SetItemBoxActiveState(false);
                    RandomisePosition();
                }

            }
        }
    }

    public int RandomAbility()
    {
        return Random.Range(0, numberOfAbilities - 1);
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
       bool ballIsCollider = collider.gameObject.CompareTag("Ball") || collider.gameObject.CompareTag("FakeBall");

        if (ballIsCollider)
        {
            Ball ballComponent = collider.gameObject.GetComponent<Ball>();

            if (ballComponent.lastHitByPlayer1)
            {
                player1.GetComponent<AbilityHolder>().GrantAbility(RandomAbility());
            }
            else
            {
                if (isSingleplayer)
                {
                    player2.GetComponent<AIAbilityHolder>().GrantAbility(RandomAbility());
                }
                else
                {
                    player2.GetComponent<AbilityHolder>().GrantAbility(RandomAbility());
                }
            }

            SetItemBoxActiveState(false);
            timer = cooldown;
        }
    }

    private void RandomisePosition()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-4, 4), Random.Range(-3, 3), 0);
        transform.position = randomPosition;
    }

    private void SetItemBoxActiveState(bool active)
    {
        itemBox.GetComponent<BoxCollider2D>().enabled = active;
        itemBox.GetComponent<SpriteRenderer>().enabled = active;
        itemBoxIsActive = active;
    }
}
