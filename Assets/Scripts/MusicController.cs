using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Button soundOn;
    [SerializeField] private Button soundOff;
    private AudioSource audio;
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip startgamesound;
    [SerializeField] private AudioClip click;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        soundOn.onClick.AddListener(delegate { TurnMusicOff(); });
        soundOff.onClick.AddListener(delegate { TurnMusicOn(); });
        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.Save();
        audio.clip = menuMusic;
        audio.Play();
        audio.loop = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void TurnMusicOn()
    {
        soundOff.gameObject.SetActive(false);
        soundOn.gameObject.SetActive(true);
        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.Save();
    }

    void TurnMusicOff()
    {
        soundOff.gameObject.SetActive(true);
        soundOn.gameObject.SetActive(false);
        PlayerPrefs.SetInt("music", 0);
        PlayerPrefs.Save();
    }

    public void StartSound()
    {
        audio.clip = startgamesound;
        audio.Play();
        audio.loop = false;
    }

    public void Click()
    {
        audio.PlayOneShot(click, 0.5f);
    }
}
