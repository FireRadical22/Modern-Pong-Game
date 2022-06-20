using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    public Slider volumeSlider;

    public AudioSource EffectClip;
    private static readonly string EffectName = "musicVolume"; 
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey(EffectName))
        {
            PlayerPrefs.SetFloat(EffectName, 1.0f);
            Load();
        } else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        EffectClip.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
