using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static GameObject ImpassableSound;
    public static GameObject InvisBallSound;
    public static GameObject BounceBallSound;
    public static GameObject MultiBallSound;
    public static GameObject SlingshotSound;
    public static GameObject TimeStopSound;

    public void Start()
    {
        ImpassableSound = GameObject.Find("ImpassableSound");
        InvisBallSound = GameObject.Find("InvisBallSound");
        BounceBallSound = GameObject.Find("BounceBallSound");
        MultiBallSound = GameObject.Find("MultiBallSound");
        SlingshotSound = GameObject.Find("SlingshotSound");
        TimeStopSound = GameObject.Find("TimeStopSound");
    }

    public static void PlayImpassableSound()
    {
        ImpassableSound.GetComponent<AudioSource>().Play();
    }

    public static void PlayInvisBallSound()
    {
        InvisBallSound.GetComponent<AudioSource>().Play();
    }

    public static void PlayBounceBallSound()
    {
        BounceBallSound.GetComponent<AudioSource>().Play();
    }

    public static void PlayMultiBallSound()
    {
        MultiBallSound.GetComponent<AudioSource>().Play();
    }

    public static void PlaySlingshotSound()
    {
        SlingshotSound.GetComponent<AudioSource>().Play();
    }

    public static void PlayTimeStopSound()
    {
        TimeStopSound.GetComponent<AudioSource>().Play();
    }

}
