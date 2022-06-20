using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScene : MonoBehaviour
{
    public GameObject Header;
    public GameObject Result;

    void Start()
    {
        Header.GetComponent<TextMeshProUGUI>().text = StateNameController.heading;
        Result.GetComponent<TextMeshProUGUI>().text = StateNameController.winner;
    }
    
}
