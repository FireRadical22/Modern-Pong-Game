using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityHolder : MonoBehaviour
{
    public KeyCode changeAbilitySlotKey;
    public KeyCode activateAbilityKey;

    public GameObject[] AbilityIcons;
    public GameObject[] AbilityBorders;
    public GameObject controlsInfo;
    public GameObject InfoBox;
    public GameObject InfoText;

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
    private Ability currentAbilityInUse;
    private Image currentAbilityIcon;
    private bool abilityIsActive;
    private GameObject currentAffectedObject;
    private float timer;

    public void Start()
    {
        heldAbilities = new int[] { -1, -1, -1 };
        UpdateAbilityUI();
        UpdateControlsInfo();
        currentSelectedAbility = 0;
    }

    private void UpdateControlsInfo()
    {
        string displayText = changeAbilitySlotKey.ToString() + " to Change Ability"
                    + "\n" + activateAbilityKey.ToString() + " to Activate Ability";


        controlsInfo.GetComponent<TextMeshProUGUI>().text = displayText;
    }

    public void Update()
    {
        if (!abilityIsActive)
        {
            if (Input.GetKeyDown(activateAbilityKey) && HasAbility(currentSelectedAbility))
            {
                currentAbilityInUse = catalogue
                                        .GetComponent<AbilityCatalogue>()
                                        .GetAbility(heldAbilities[currentSelectedAbility]);

                currentAbilityInUseIndex = currentSelectedAbility;

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
                    currentAbilityInUse.Activate(currentAffectedObject);
                    currentAbilityIcon.fillAmount = 1;
                }
                else
                {
                    currentAbilityIcon.color = new Color32(0, 255, 0, 255);
                }
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

        if (Input.GetKeyDown(changeAbilitySlotKey))
        {
            AbilityBorders[currentSelectedAbility].GetComponent<SpriteRenderer>().sprite = ABILITYNOTSELECTED;
            currentSelectedAbility = (currentSelectedAbility + 1) % heldAbilities.Length;
            AbilityBorders[currentSelectedAbility].GetComponent<SpriteRenderer>().sprite = ABILITYSELECTED;
            UpdateAbilityDescription();
        }
    }

    private void UpdateAbilityDescription()
    {
        if (HasAbility(currentSelectedAbility))
        {
            InfoText.GetComponent<TextMeshProUGUI>().text = catalogue.GetComponent<AbilityCatalogue>().GetDescription(currentSelectedAbility);
            InfoBox.SetActive(true);
            InfoText.SetActive(true);
        }
        else
        {
            InfoBox.SetActive(false);
            InfoText.SetActive(false);
        }

        
    }

    public void ResetAllAbilities()
    {
        DisableAbility();
        heldAbilities = new int[] { -1, -1, -1 };
        UpdateAbilityUI();
        UpdateAbilityDescription();
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
                otherPlayer.GetComponent<AIAbilityHolder>().DisableAbility();
            }
            else
            {
                otherPlayer.GetComponent<AbilityHolder>().DisableAbility();
            }
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

    public void DisableAbility()
    {
        if (currentAbilityInUse != null)
        {
            currentAbilityInUse.Deactivate(currentAffectedObject);
        }

        if (currentAbilityInUse is CollisionAbility)
        {
            currentAbilityIcon.color = new Color32(255, 255, 255, 255);
        }

        if (currentAbilityInUseIndex != -1)
        {
            heldAbilities[currentAbilityInUseIndex] = -1;
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
        return heldAbilities[i] != -1;
    }

    public void GrantAbility(int ability)
    {
        for (int i = 0; i < heldAbilities.Length; i++)
        {
            if (!HasAbility(i))
            {
                heldAbilities[i] = ability;
                UpdateAbilityUI();
                return;
            }
        }
        UpdateAbilityDescription();
    }

}