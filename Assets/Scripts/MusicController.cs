using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Button soundOn;
    [SerializeField] private Button soundOff;

    // Start is called before the first frame update
    void Start()
    {
        soundOn.onClick.AddListener(delegate { TurnMusicOff(); });
        soundOff.onClick.AddListener(delegate { TurnMusicOn(); });
        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.Save();
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
}
