using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
	[SerializeField] private GameObject randomEnemy;
	[SerializeField] private GameObject followerEnemy;
	private GameObject player;
	private GameObject enemy;
	public static int enemyCount = 0;
	private int wave = 0;
	private Vector2[] locations;

	[SerializeField] private Text waveCount;
	[SerializeField] private Text healthCount;
	[SerializeField] private Text highScore;

	private int highScoreValue;
	public Button quit;
	public Button restart;
	public GameObject gameover;

	void Start()
	{
		DisplayHighScore();
		gameover.SetActive(false);
		wave = 0;
		enemyCount = 0;
		locations = new Vector2[10];
		locations[0] = new Vector2(0f, 4.2f);
		locations[1] = new Vector2(0f, -4.2f);
		locations[2] = new Vector2(5.8f, 1.6f);
		locations[3] = new Vector2(-5.8f, -1.6f);
		locations[4] = new Vector2(5.8f, -1.6f);
		locations[5] = new Vector2(-5.8f, 1.6f);
		locations[6] = new Vector2(-2.5f, 4.2f);
		locations[7] = new Vector2(2.5f, -4.2f);
		locations[8] = new Vector2(2.5f, 4.2f);
		locations[9] = new Vector2(-2.5f, -4.2f);

		quit.onClick.AddListener(delegate { Quit(); });
		restart.onClick.AddListener(delegate { SceneManager.LoadScene("Game"); });
	}

	void Update()
	{
		UpdateHealth();
		if (enemyCount == 0)
		{
			enemyCount = 1;
			StartCoroutine(NewWave());
		}

	}

	public void DecreaseEnemyCount()
	{
		enemyCount--;
	}

	public IEnumerator NewWave()
	{
		wave++;

		yield return new WaitForSeconds(5.0f);
		UpdateScore();
		var dodgeballs = FindObjectsOfType<Dodgeball>();
		foreach (Dodgeball d in dodgeballs)
		{
			Destroy(d.gameObject);
		}
		Vector2 player = GameObject.Find("Player").transform.position;

		if (wave <= 10)
		{
			for (int i = wave; i > 0; i--)
			{
				if (i % 2 == 0)
				{
					enemy = Instantiate(randomEnemy) as GameObject;
				}
				else
				{
					enemy = Instantiate(followerEnemy) as GameObject;
				}
				Vector2 location = locations[i - 1];
				enemy.transform.position = location;
				float angle = Random.Range(0, 360);
				enemy.transform.Rotate(0, 0, angle);
				enemyCount++;
			}
		}
		else
		{
			for (int i = 10; i > 0; i--)
			{
				if (i % 2 == 0)
				{
					enemy = Instantiate(randomEnemy) as GameObject;
				}
				else
				{
					enemy = Instantiate(followerEnemy) as GameObject;
				}
				Vector2 location = locations[i - 1];
				enemy.transform.position = location;
				float angle = Random.Range(0, 360);
				enemy.transform.Rotate(0, 0, angle);
				enemyCount++;
			}
		}
		enemyCount -= 1;

	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Lose()
	{
		if(wave > PlayerPrefs.GetInt("highscore"))
        {
			PlayerPrefs.SetInt("highscore", wave);
			PlayerPrefs.Save();
        }
		gameover.SetActive(true);
	}

	public void UpdateScore()
	{
		waveCount.text = "Wave: " + wave;
	}

	public void UpdateHealth()
	{
		healthCount.text = "Health: " + FindObjectOfType<PlayerCharacter>().getHealth();
	}

	public void DisplayHighScore()
	{
		if(PlayerPrefs.GetInt("highscore") == 0)
        {
			PlayerPrefs.SetInt("highscore", 0);
			PlayerPrefs.Save();
		}
		highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore");
	}
}
