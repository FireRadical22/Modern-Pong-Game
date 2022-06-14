using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityHolder : MonoBehaviour
{
    [SerializeField]
    public KeyCode[] InputKeys;

    [SerializeField]
    public GameObject[] AbilityIcons;

    [SerializeField]
    public GameObject[] KeycodeIcons;

    public GameObject catalogue;
    public bool isPlayer1;
    public GameObject player;
    public GameObject otherPlayer;
    public GameObject ball;
    public Sprite EMPTY;
    public float timeAbilityActiveTime;
    public bool isBot;

    private int[] heldAbilities;
    private int currentAbilityInUseIndex;
    private Ability currentAbilityInUse;
    private Image currentAbilityIcon;
    private bool abilityIsActive;
    private GameObject currentAffectedObject;
    private float timer;

    public void Start()
    {
        heldAbilities = new int[] { 0, -1, -1 }; //change this for debugging

        UpdateUI();
        UpdateKeycodeUI();
    }

    public void Update()
    {
        if (!abilityIsActive)
        {
            for (int i = 0; i < InputKeys.Length; i++)
            {
                if (Input.GetKeyDown(InputKeys[i]) && HasAbility(i))
                {
                    currentAbilityInUse = catalogue
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

                    currentAbilityIcon = AbilityIcons[i].GetComponent<Image>();

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

        UpdateUI();
    }

    public void UpdateUI()
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

    private void UpdateKeycodeUI()
    {
        for (int i = 0; i < KeycodeIcons.Length; i++)
        {
            KeycodeIcons[i].GetComponent<TextMeshProUGUI>().text = InputKeys[i].ToString();
        }
    }

    public void GrantAbility(int ability)
    {
        for (int i = 0; i < heldAbilities.Length; i++)
        {
            if (!HasAbility(i))
            {
                heldAbilities[i] = ability;
                UpdateUI();
                return;
            }
        }
    }

}