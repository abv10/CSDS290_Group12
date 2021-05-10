using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
	[SerializeField] private GameObject randomEnemy;
	[SerializeField] private GameObject followerEnemy;
	[SerializeField] private GameObject shieldEnemy;
	private GameObject player;
	private GameObject enemy;
	public static int enemyCount = 0;
	private int wave = 0;
	private Vector2[] locations;

	private int speedToAdd = 0;
	private int forceToAdd = 0;

	[SerializeField] private GameObject healthPowerUpPrefab;
	private GameObject healthPowerUp;
	private bool healthspawn;

	[SerializeField] private GameObject shieldPrefab;
	private GameObject shield;
	private bool shieldspawn;
	private bool shieldactive;
	private float shieldTimeRemaining = 0;

	private bool explosiveballactive;

	private bool antibounce;
	private bool antibouncedone;

	[SerializeField] private Text waveCount;
	[SerializeField] private Text healthCount;
	[SerializeField] private Text highScore;
	[SerializeField] private Text countdown;
	[SerializeField] private Text streakCount;
	[SerializeField] private Text shieldtext;
	[SerializeField] private Text explosiveballtext;
	[SerializeField] private Text antibouncetext;
	[SerializeField] private Text antibouncecountdown;
	private int highScoreValue;
	public Button quit;
	public Button restart;
	public GameObject gameover;
	[SerializeField] private Text gameovertext;
	[SerializeField] private Text finalwave;
	[SerializeField] private Text higheststreak;
	private int streakhighscore;

	void Start()
	{
		StartCoroutine(CountDown());
		gameover.SetActive(false);
		wave = 0;
		enemyCount = 0;
		streakhighscore = 0;
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

		healthspawn = false;

		shieldtext.color = new Color32(217, 6, 6, 255);
		explosiveballtext.color = new Color32(217, 6, 6, 255);
		antibouncetext.color = new Color32(217, 6, 6, 255);
		shieldspawn = true;
		shieldactive = false;

		explosiveballactive = false;

		antibounce = false;
		antibouncecountdown.gameObject.SetActive(false);
		antibouncecountdown.color = new Color32(0, 200, 3, 255);
		antibouncedone = false;

		quit.onClick.AddListener(delegate { SceneManager.LoadScene("StartScreen"); });
		restart.onClick.AddListener(delegate { SceneManager.LoadScene("Game"); });
	}

	void Update()
	{
		UpdateHealth();
		UpdateStreak();

		//1 Health Power Up spawns every 3 waves
		if (wave != 0 && wave % 3 == 0 && healthspawn == true)
		{
			healthPowerUp = Instantiate(healthPowerUpPrefab) as GameObject;
			healthPowerUp.transform.position = new Vector2(0, 0);
			healthspawn = false;
		}

		//Streak Power Ups
		if (FindObjectOfType<PlayerCharacter>().getStreak() == 0)
		{
			if (shieldactive == false)
			{
				shieldspawn = true;
				shieldtext.color = new Color32(217, 6, 6, 255);
			}
			explosiveballactive = false;
			explosiveballtext.color = new Color32(217, 6, 6, 255);
			antibouncetext.color = new Color32(217, 6, 6, 255);
		}
		if (shieldTimeRemaining > 0)
		{
			shieldTimeRemaining -= Time.deltaTime;
		}
		else
		{
			shieldTimeRemaining = 0f;
		}
		if (FindObjectOfType<PlayerCharacter>().getStreak() == 3 && shieldspawn == true)
		{
			StartCoroutine(ShieldActivate(3.0f));
		}

		if (FindObjectOfType<PlayerCharacter>().getStreak() == 6 && explosiveballactive == false)
		{
			explosiveballactive = true;
			FindObjectOfType<PlayerThrowing>().setExplosive(true);
			explosiveballtext.color = new Color32(0, 200, 3, 255);
			//StartCoroutine(ExplosiveBall());
		}

		if (FindObjectOfType<PlayerCharacter>().getStreak() == 10 && antibounce == false && antibouncedone == false)
        {
			antibounce = true;
			antibouncetext.color = new Color32(0, 200, 3, 255);
			StartCoroutine(AntiBounce());
		}

		if ((FindObjectOfType<PlayerCharacter>().getStreak() % 10) != 0)
        {
			antibouncedone = false;
        }

		//New Wave
		if (enemyCount == 0)
		{
			StartCoroutine(CountDown());
			enemyCount = 1;
			StartCoroutine(NewWave(shieldTimeRemaining));
			healthspawn = true;
		}
	}

	public void DecreaseEnemyCount()
	{
		enemyCount--;
	}

	public IEnumerator NewWave(float shieldReactivateTime)
	{
		wave++;

		yield return new WaitForSeconds(5.0f);
		UpdateScore();
		var dodgeballs = FindObjectsOfType<Dodgeball>();
		foreach (Dodgeball d in dodgeballs)
		{
			Destroy(d.gameObject);
		}
		//Reactivate Shield
		if (shieldReactivateTime != 0)
		{
			StartCoroutine(ShieldActivate(shieldReactivateTime));
		}

		Vector2 player = GameObject.Find("Player").transform.position;
		var availableLocations = new ArrayList() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		if (wave <= 10)
		{
			for (int i = wave; i > 0; i--)
			{
				int randomizer = UnityEngine.Random.Range(1, 10);
				if (randomizer < 6)
				{
					enemy = Instantiate(followerEnemy) as GameObject;
					enemy.GetComponent<Enemy>().increaseSpeed(speedToAdd);
					enemy.GetComponent<Enemy>().increaseForce(forceToAdd);
				}
				else if (randomizer < 9)
				{
					enemy = Instantiate(randomEnemy) as GameObject;
					enemy.GetComponent<RandomDirectionEnemy>().increaseSpeed(speedToAdd);
					enemy.GetComponent<RandomDirectionEnemy>().increaseForce(forceToAdd);
				}
				else
				{
					enemy = Instantiate(shieldEnemy) as GameObject;
					enemy.GetComponent<ShieldEnemy>().increaseSpeed(speedToAdd);
					enemy.GetComponent<ShieldEnemy>().increaseForce(forceToAdd);
				}

				//Prevents the player from predicting where the enemies will spawn
				int locationNumber = Random.Range(0, availableLocations.Count);
				var locIndex = availableLocations[locationNumber];
				availableLocations.Remove(locIndex);
				Vector2 location = locations[(int)locIndex];

				enemy.transform.position = location;
				float angle = UnityEngine.Random.Range(0, 360);
				enemy.transform.Rotate(0, 0, angle);
				enemyCount++;
			}
			speedToAdd++;
			forceToAdd++;
		}
		else
		{
			int randomizer = Random.Range(1, 10);
			for (int i = 10; i > 0; i--)
			{
				if (randomizer < 6)
				{
					enemy = Instantiate(followerEnemy) as GameObject;
					enemy.GetComponent<Enemy>().increaseSpeed(speedToAdd);
					enemy.GetComponent<Enemy>().increaseForce(forceToAdd);
				}
				else if (randomizer < 9)
				{
					enemy = Instantiate(randomEnemy) as GameObject;
					enemy.GetComponent<RandomDirectionEnemy>().increaseSpeed(speedToAdd);
					enemy.GetComponent<RandomDirectionEnemy>().increaseForce(forceToAdd);
				}
				else
				{
					enemy = Instantiate(shieldEnemy) as GameObject;
					enemy.GetComponent<ShieldEnemy>().increaseSpeed(speedToAdd);
					enemy.GetComponent<ShieldEnemy>().increaseForce(forceToAdd);
				}
				//Prevents the player from predicting where the enemies will spawn
				int locationNumber = Random.Range(0, availableLocations.Count);
				var locIndex = availableLocations[locationNumber];
				availableLocations.Remove(locIndex);
				Vector2 location = locations[(int)locIndex];

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
		if (wave > PlayerPrefs.GetInt("highscore"))
		{
			PlayerPrefs.SetInt("highscore", wave);
			PlayerPrefs.SetString("date", System.DateTime.Now.ToLongDateString());
			PlayerPrefs.Save();
		}
 		if(streakhighscore > PlayerPrefs.GetInt("streak", 0))
        {
			PlayerPrefs.SetInt("streak", streakhighscore);
			PlayerPrefs.SetString("streakDate", System.DateTime.Now.ToLongDateString());
			PlayerPrefs.Save();
		}
		gameover.SetActive(true);
		StartCoroutine(Gameover());
		finalwave.text = "Score: " + wave;
		higheststreak.text = "Best Streak: " + streakhighscore;
		DisplayHighScore();
		FindObjectOfType<MusicControllerGame>().GameOver();
	}

	public void UpdateScore()
	{
		waveCount.text = "Wave: " + wave;
	}

	public void UpdateHealth()
	{
		healthCount.text = "Health: " + FindObjectOfType<PlayerCharacter>().getHealth();
	}

	public void UpdateStreak()
	{
		streakCount.text = "Streak: " + FindObjectOfType<PlayerCharacter>().getStreak();
		if (streakhighscore < FindObjectOfType<PlayerCharacter>().getStreak())
		{
			streakhighscore = FindObjectOfType<PlayerCharacter>().getStreak();
		}
	}

	public int getWave()
	{
		return wave;
	}

	public bool getShieldActive()
	{
		return shieldactive;
	}

	public IEnumerator ShieldActivate(float time)
	{
		shieldTimeRemaining = time;
		shield = Instantiate(shieldPrefab) as GameObject;
		shield.transform.position = FindObjectOfType<PlayerCharacter>().transform.position;
		shieldspawn = false;
		shieldactive = true;
		shieldtext.color = new Color32(0, 200, 3, 255);

		yield return new WaitForSeconds(time);

		shieldactive = false;
		Destroy(shield);
		if (FindObjectOfType<PlayerCharacter>().getStreak() < 3)
		{
			shieldspawn = true;
			shieldtext.color = new Color32(217, 6, 6, 255);
		}
		//shieldtext.color = new Color32(217, 6, 6, 255);
	}

	public IEnumerator Gameover()
	{
		while (true)
		{
			gameovertext.text = "GAMEOVER";

			yield return new WaitForSeconds(0.75f);

			gameovertext.text = "GAMEOVER_";

			yield return new WaitForSeconds(0.75f);
		}
	}

	public void DisplayHighScore()
	{
		if (PlayerPrefs.GetInt("highscore") == 0)
		{
			PlayerPrefs.SetInt("highscore", 0);
			PlayerPrefs.Save();
		}
		highScore.text = "High Score: " + PlayerPrefs.GetInt("highscore") + "\n" + "High Streak: " + PlayerPrefs.GetInt("streak");
	}

	public IEnumerator CountDown()
	{
		countdown.gameObject.SetActive(true);
		countdown.text = "Next Wave In:\n5";
		yield return new WaitForSeconds(1.0f);
		countdown.text = "Next Wave In:\n4";
		yield return new WaitForSeconds(1.0f);
		countdown.text = "Next Wave In:\n3";
		yield return new WaitForSeconds(1.0f);
		countdown.text = "Next Wave In:\n2";
		yield return new WaitForSeconds(1.0f);
		countdown.text = "Next Wave In:\n1";
		yield return new WaitForSeconds(1.0f);
		countdown.gameObject.SetActive(false);
	}

	public IEnumerator AntiBounce()
    {
		antibouncecountdown.gameObject.SetActive(true);
		antibouncecountdown.text = "5";
		yield return new WaitForSeconds(1.0f);
		antibouncecountdown.text = "4";
		yield return new WaitForSeconds(1.0f);
		antibouncecountdown.text = "3";
		yield return new WaitForSeconds(1.0f);
		antibouncecountdown.text = "2";
		yield return new WaitForSeconds(1.0f);
		antibouncecountdown.text = "1";
		yield return new WaitForSeconds(1.0f);
		antibouncecountdown.gameObject.SetActive(false);

		//antibouncetext.color = new Color32(217, 6, 6, 255);
		antibounce = false;
		//antibouncedone = true;
	}

	public bool getAntiBounce()
    {
		return antibounce;
    }

}