using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIAbilityHolder : MonoBehaviour
{
    //[SerializeField]
    //public KeyCode[] InputKeys;

    [SerializeField]
    public GameObject[] AbilityIcons;

    //[SerializeField]
    //public GameObject[] KeycodeIcons;

    public GameObject catalogue;
    public bool isPlayer1;
    public GameObject player;
    public GameObject otherPlayer;
    public GameObject ball;
    public Sprite EMPTY;
    public float timeAbilityActiveTime;

    private int[] heldAbilities;
    private int currentAbilityInUseIndex;
    private Ability currentAbilityInUse;
    private Image currentAbilityIcon;
    private bool abilityIsActive;
    private GameObject currentAffectedObject;
    private float timer;

    private int SlotChosen = -1;

    public void Start()
    {
        //heldAbilities = new int[] { -1, -1, -1 }; //change this for debugging
        heldAbilities = new int[] { 0, 0, 0 };

        UpdateUI();
        //UpdateKeycodeUI();
    }

    public void Update()
    {
        if (!abilityIsActive)
        {
            //for (int i = 0; i < heldAbilities.Length; i++)
            //{
            while (SlotChosen >= 0)
            {
                if (HasAbility(SlotChosen))
                {
                    currentAbilityInUse = catalogue
                                          .GetComponent<AbilityCatalogue>()
                                          .GetAbility(heldAbilities[SlotChosen]);

                    currentAbilityInUseIndex = SlotChosen;

                    if (currentAbilityInUse is Impassable)
                    {
                        currentAffectedObject = player;
                    }
                    else
                    {
                        currentAffectedObject = catalogue
                                                .GetComponent<AbilityCatalogue>()
                                                .GetAffectedObject(heldAbilities[SlotChosen]);
                    }

                    currentAbilityIcon = AbilityIcons[SlotChosen].GetComponent<Image>();

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
                } else 
                {
                    SlotChosen = FirstAvailAbility();
                    if (SlotChosen < 0){
                        return;
                    }
                }
            }
        }
        else if (currentAbilityInUse is TimeAbility)
        {
            timer -= Time.deltaTime;
            currentAbilityIcon.fillAmount -= (1 / timeAbilityActiveTime) * Time.deltaTime;

            if (timer <= 0)
            {
                DisableAbility();
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
            otherPlayer.GetComponent<AIAbilityHolder>().DisableAbility();
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
                AbilityIcons[i].GetComponent<Image>().sprite = EMPTY;
            }
            else
            {
                AbilityIcons[i].GetComponent<Image>().sprite = catalogue
                                                               .GetComponent<AbilityCatalogue>()
                                                               .GetIcon(heldAbilities[i]);
            }

        }
    }

    private bool HasAbility(int i)
    {
        return heldAbilities[i] != -1 || i < 0 || i > heldAbilities.Length;
    }

    //private void UpdateKeycodeUI()
    //{
    //    for (int i = 0; i < KeycodeIcons.Length; i++)
    //    {
    //        KeycodeIcons[i].GetComponent<TextMeshProUGUI>().text = InputKeys[i].ToString();
    //    }
    //}

    public void GetAbility(int ability)
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

    private int FirstAvailAbility()
    {
        for (int i = 0; i < heldAbilities.Length; i++)
        {
            if (HasAbility(i))
            {
                return i;
            }
        }

        return -1;
    }
}
