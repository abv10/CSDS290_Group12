using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;
    
    public void Start()
    {
        Slider slider = FindObjectOfType<Slider>();
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.99f);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.Save();
    }
}
