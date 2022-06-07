using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField]
    public KeyCode[] InputKeys;

    public GameObject catalogue;
    public bool isPlayer1;
    public GameObject player;
    public GameObject otherPlayer;
    public GameObject ball;

    [SerializeField]
    public GameObject[] AbilityIcons;

    public Sprite EMPTY;

    private int[] heldAbilities;
    private int currentAbilityInUseIndex;
    private Ability currentAbilityInUse;
    private bool abilityIsActive;
    private GameObject currentAffectedObject;

    private float timer;

    private void Start()
    {
        //heldAbilities = new int[] { -1, -1, -1 }; //change this for debugging
        heldAbilities = new int[] { 0, 2, 3 };
        UpdateUI();
    }
    private void Update()
    {
        for (int i = 0; i < InputKeys.Length; i++)
        {
            if (Input.GetKeyDown(InputKeys[i]) && HasAbility(i) && !abilityIsActive)
            {
                currentAbilityInUse =
                    catalogue
                    .GetComponent<AbilityCatalogue>()
                    .GetAbility(heldAbilities[i]);

                currentAbilityInUseIndex = i;

                if (currentAbilityInUse is Impassable)
                {
                    currentAffectedObject = player;
                }
                else
                {
                    currentAffectedObject = catalogue
                                            .GetComponent<AbilityCatalogue>()
                                            .GetAffectedObject(heldAbilities[i]);
                }

                abilityIsActive = true;

                if (currentAbilityInUse is TimeAbility)
                {
                    timer = 5; //Change this later, for some reason cannot access active time 
                    currentAbilityInUse.Activate(currentAffectedObject);
                }
            }
        }

        if (abilityIsActive)
        {
            if (currentAbilityInUse is TimeAbility)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    DisableAbility();
                }

            }
        }
    }

    public void ResetAllAbilities()
    {
        DisableAbility();
        heldAbilities = new int[] { -1, -1, -1 };
        UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        bool ballIsCollider = collider.gameObject.CompareTag("Ball");

        if (!ballIsCollider)
            return;

        Ball ballComponent = collider.gameObject.GetComponent<Ball>();

        bool ballIsModifiedByOtherPlayer = 
            isPlayer1 ? ballComponent.isModifiedByPlayer2
                      : ballComponent.isModifiedByPlayer1;

        if (ballIsModifiedByOtherPlayer)
        {
            otherPlayer.GetComponent<AbilityHolder>().DisableAbility();
        }

        if (abilityIsActive && currentAbilityInUse is CollisionAbility)
        {
            currentAbilityInUse.Activate(currentAffectedObject);

            if (isPlayer1)
            {
                ballComponent.isModifiedByPlayer1 = true;
            }
            else
            {
                ballComponent.isModifiedByPlayer2 = true;
            }
        }
    }

    private void DisableAbility()
    {
        if (currentAbilityInUse != null)
        {
            currentAbilityInUse.Deactivate(currentAffectedObject);
        }

        if (currentAbilityInUseIndex != -1)
        {
            heldAbilities[currentAbilityInUseIndex] = -1;
        }

        currentAbilityInUse = null;
        currentAbilityInUseIndex = -1;
        abilityIsActive = false;
        currentAffectedObject = null;

        UpdateUI();

        if (isPlayer1)
        {
            ball.GetComponent<Ball>().isModifiedByPlayer1 = false;
        }
        else
        {
            ball.GetComponent<Ball>().isModifiedByPlayer2 = false;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < AbilityIcons.Length; i++)
        {
            if (heldAbilities[i] == -1)
            {
                AbilityIcons[i].GetComponent<Image>().sprite = EMPTY;
            }
            else
            {
                AbilityIcons[i].GetComponent<Image>().sprite = 
                    catalogue
                    .GetComponent<AbilityCatalogue>()
                    .GetIcon(heldAbilities[i]);
            }

        }
    }

    private bool HasAbility(int i)
    {
        return heldAbilities[i] != -1;
    }

}