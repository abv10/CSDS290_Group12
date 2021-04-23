using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private Button start;
    [SerializeField] private Button viewInstructions;
    [SerializeField] private Button viewHighScore;
    [SerializeField] private Button toStartFromInstructions;
    [SerializeField] private Button resetScore;
    [SerializeField] private Button toStartFromHighScore;
    [SerializeField] private Text highScoreDisplay;


    [SerializeField] private Canvas instructions;
    [SerializeField] private Canvas startmenu;
    [SerializeField] private Canvas highScoreMenu;


    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(delegate { SceneManager.LoadScene("Game"); });
        viewInstructions.onClick.AddListener(delegate { ViewInstructions(); });
        toStartFromInstructions.onClick.AddListener(delegate { BackToStart(); });
        toStartFromHighScore.onClick.AddListener(delegate { BackToStart(); });
        resetScore.onClick.AddListener(delegate { ResetHighScore(); });
        viewHighScore.onClick.AddListener(delegate { ToHighScorePage(); });

        DisplayHighScore();
        highScoreMenu.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        startmenu.gameObject.SetActive(true);
    }

    private void ResetHighScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
        DisplayHighScore();
    }

    private void DisplayHighScore()
    {
        highScoreDisplay.text = "High Score:" + PlayerPrefs.GetInt("highscore");
    }

    void ViewInstructions()
    {
        instructions.gameObject.SetActive(true);
        startmenu.gameObject.SetActive(false);
        highScoreMenu.gameObject.SetActive(false);
    }

    void BackToStart()
    {
        instructions.gameObject.SetActive(false);
        startmenu.gameObject.SetActive(true);
        highScoreMenu.gameObject.SetActive(false);
    }

    void ToHighScorePage()
    {
        instructions.gameObject.SetActive(false);
        startmenu.gameObject.SetActive(false);
        highScoreMenu.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
