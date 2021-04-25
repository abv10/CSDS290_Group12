using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MusicControllerGame : MonoBehaviour
{
    private AudioSource audio;
    [SerializeField] private AudioClip gameMusic;
    [SerializeField] private AudioClip gameover;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.clip = gameMusic;
        audio.Play();
        audio.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        audio.clip = gameover;
        audio.Play();
        audio.loop = false;
    }
}
