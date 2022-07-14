using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AIAbilityHolder : MonoBehaviour
{
    public KeyCode changeAbilitySlotKey;
    public KeyCode activateAbilityKey;

    public GameObject[] AbilityIcons;
    public GameObject[] AbilityBorders;

    public GameObject catalogue;

    public bool isBot;
    public bool isPlayer1;
    public GameObject player;
    public GameObject otherPlayer;
    public GameObject ball;
    public float timeAbilityActiveTime;

    public Sprite ABILITYSELECTED;
    public Sprite ABILITYNOTSELECTED;

    private int[] heldAbilities;
    private int currentSelectedAbility;
    private int currentAbilityInUseIndex;
    public Ability currentAbilityInUse;
    private Image currentAbilityIcon;
    private bool abilityIsActive;
    private GameObject currentAffectedObject;
    private float timer;

    delegate void Activate(GameObject affectedObject);
    Activate activate;

    delegate void Deactivate(GameObject affectedObject);
    Deactivate deactivate;

    public void Start()
    {
        heldAbilities = new int[] { -1, -1, -1 };
        UpdateAbilityUI();
        currentSelectedAbility = 1;
    }

    public void Update()
    {
        if (!isActivated())
        {

            if (HasAbility(currentSelectedAbility)) 
            {
               
                currentAbilityInUse = catalogue
                                       .GetComponent<AbilityCatalogue>()
                                       .GetAbility(heldAbilities[currentSelectedAbility]);

                activate = currentAbilityInUse.Activate;
                deactivate = currentAbilityInUse.Deactivate;

                if (currentAbilityInUse is Impassable)
                {
                    currentAffectedObject = player;
                }
                else
                {
                    currentAffectedObject = catalogue
                                            .GetComponent<AbilityCatalogue>()
                                            .GetAffectedObject(heldAbilities[currentSelectedAbility]);
                }

                currentAbilityIcon = AbilityIcons[currentSelectedAbility].GetComponent<Image>();

                abilityIsActive = true;

                if (currentAbilityInUse is TimeAbility)
                {
                    timer = timeAbilityActiveTime; //Change this later, for some reason cannot access active time 
                    activate(currentAffectedObject);
                    currentAbilityIcon.fillAmount = 1;
                }
                else
                {
                    currentAbilityIcon.color = new Color32(0, 255, 0, 255);
                }
            } else if (FirstAvail() >= 0)
            {
                AISelect(FirstAvail());
            }
        }
        else
        {
            if (currentAbilityInUse is TimeAbility)
            {
                timer -= Time.deltaTime;
                currentAbilityIcon.fillAmount -= (1 / timeAbilityActiveTime) * Time.deltaTime;

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
        UpdateAbilityUI();
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
            if (isBot)
            {
                otherPlayer.GetComponent<AbilityHolder>().DisableAbility();
            }
            else
            {
                otherPlayer.GetComponent<AIAbilityHolder>().DisableAbility();
            }
        }

        if (isActivated() && currentAbilityInUse is CollisionAbility)
        {
            activate(currentAffectedObject);

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

    public void DisableAbility()
    {
        if (currentAbilityInUse != null)
        {
            deactivate(currentAffectedObject);
        }

        if (currentAbilityInUse is CollisionAbility)
        {
            currentAbilityIcon.color = new Color32(255, 255, 255, 255);
        }

        if (currentSelectedAbility != -1)
        {
            heldAbilities[currentSelectedAbility] = -1;
        }

        currentAbilityInUse = null;
        currentAbilityInUseIndex = -1;
        abilityIsActive = false;
        currentAffectedObject = null;
        currentAbilityIcon = null;

        if (isPlayer1)
        {
            ball.GetComponent<Ball>().isModifiedByPlayer1 = false;
        }
        else
        {
            ball.GetComponent<Ball>().isModifiedByPlayer2 = false;
        }
        UpdateAbilityUI();
        int nextIndex = (currentSelectedAbility + 1) % heldAbilities.Length;
        AISelect(nextIndex);
    }

    public void UpdateAbilityUI()
    {
        for (int i = 0; i < AbilityIcons.Length; i++)
        {
            if (heldAbilities[i] == -1)
            {
                AbilityIcons[i].GetComponent<Image>().fillAmount = 0;
            }
            else
            {
                AbilityIcons[i].GetComponent<Image>().sprite = catalogue
                                                               .GetComponent<AbilityCatalogue>()
                                                               .GetIcon(heldAbilities[i]);
                AbilityIcons[i].GetComponent<Image>().fillAmount = 1;
            }
        }
    }

    private bool HasAbility(int i)
    {
        return i>= 0 && heldAbilities[i] != -1;
    }

    public void GrantAbility(int ability)
    {
        for (int i = 0; i < heldAbilities.Length; i++)
        {
            if (!HasAbility(i))
            {
                heldAbilities[i] = ability;
                UpdateAbilityUI();
                currentAbilityInUseIndex = i;
                return;
            }
        }
    }

    private void AISelect(int index)
    {
        if (!AIControl(changeAbilitySlotKey) && currentSelectedAbility != index)
        {
            AbilityBorders[currentSelectedAbility].GetComponent<SpriteRenderer>().sprite = ABILITYNOTSELECTED;
            currentSelectedAbility++;
            currentSelectedAbility %= heldAbilities.Length;
            AbilityBorders[currentSelectedAbility].GetComponent<SpriteRenderer>().sprite = ABILITYSELECTED;
        }        
    }

    private bool AIControl(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            return false;
        }

        return false;
    }

    private int FirstAvail()
    {
        
        for (int i = 0; i < heldAbilities.Length; i++)
        {
            if (heldAbilities[i] >= 0)
            {
                return i;
            }
        }

        return -1;
    }

    public bool isActivated()
    {
        return abilityIsActive;
    }

    
}
