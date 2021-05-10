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
    [SerializeField] private Button quit;
    [SerializeField] private Button toStartFromInstructions;
    [SerializeField] private Button resetScore;
    [SerializeField] private Button toStartFromHighScore;
    [SerializeField] private Text highScoreDisplay;
    [SerializeField] private Text highStreakDisplay;
    [SerializeField] private RectTransform title;
    private float speed;
    private float time;


    [SerializeField] private Canvas instructions;
    [SerializeField] private Canvas startmenu;
    [SerializeField] private Canvas highScoreMenu;


    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(delegate { StartCoroutine(StartGame()); });
        viewInstructions.onClick.AddListener(delegate { FindObjectOfType<MusicController>().Click(); ViewInstructions(); });
        toStartFromInstructions.onClick.AddListener(delegate { FindObjectOfType<MusicController>().Click(); BackToStart(); });
        toStartFromHighScore.onClick.AddListener(delegate { FindObjectOfType<MusicController>().Click(); BackToStart(); });
        resetScore.onClick.AddListener(delegate { FindObjectOfType<MusicController>().Click(); ResetHighScore(); });
        viewHighScore.onClick.AddListener(delegate { FindObjectOfType<MusicController>().Click(); ToHighScorePage(); });
        quit.onClick.AddListener(delegate { FindObjectOfType<MusicController>().Click(); Quit(); });

        speed = 100;
        time = 0.5455f;
        //title.transform.position = new Vector3(960, 787, 0);

        DisplayHighScore();
        highScoreMenu.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        startmenu.gameObject.SetActive(true);
    }

    private void ResetHighScore()
    {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.SetInt("streak", 0);
        PlayerPrefs.Save();
        DisplayHighScore();
    }

    private void DisplayHighScore()
    {
        string text = "High Score: " + PlayerPrefs.GetInt("highscore");
        string streakText = "Highest Streak: " + PlayerPrefs.GetInt("streak");
        if (PlayerPrefs.GetInt("highscore") != 0)
        {
            text = text + "\n" + "Set on: " + PlayerPrefs.GetString("date", "not set yet");
            
        }
        if(PlayerPrefs.GetInt("streak") != 0)
        {
            streakText = streakText + "\n" + "Set on: " + PlayerPrefs.GetString("streakDate", "not set yet");
        }
        highScoreDisplay.text = text;
        highStreakDisplay.text = streakText;
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

    public void Quit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        //Moving game title
        title.transform.Translate(speed * Time.deltaTime, 0, 0);

        float x = title.transform.localPosition.x;

        if (time <= 0)
        {
            speed = -speed;
            time = 0.5455f;
        }
        time -= Time.deltaTime;
    }

    public IEnumerator StartGame()
    {
        FindObjectOfType<MusicController>().StartSound();

        yield return new WaitForSeconds(1.0f);

        SceneManager.LoadScene("Game");
    }

}