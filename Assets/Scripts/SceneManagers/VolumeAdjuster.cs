using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeAdjuster : MonoBehaviour
{
    public AudioSource EffectClip;
    private static readonly string EffectName = "musicVolume";
    private float maxVolume = 0.261f;
    private float volume;
    // Start is called before the first frame update
    void Awake()
    {
        ContinueSetting();
    }

    // Update is called once per frame
    void ContinueSetting()
    {
        volume = PlayerPrefs.GetFloat(EffectName);
        EffectClip.volume = maxVolume * volume;
    }
}
