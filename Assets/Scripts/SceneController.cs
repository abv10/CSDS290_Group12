using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	private GameObject player;
	private GameObject enemy;
	public static int enemyCount = 0;
	private int wave = 0;
	private Vector2[] locations;

	void Start()
	{
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
	}

	void Update()
	{
		if (enemyCount == 0)
		{
			enemyCount = 1;
			StartCoroutine(NewWave());
		}

	}

	public IEnumerator NewWave()
	{
		wave++;
		Dodgeball.waveOver = true;

		yield return new WaitForSeconds(5.0f);

		Vector2 player = GameObject.Find("Player").transform.position;

		if (wave <= 10)
		{
			for (int i = wave; i > 0; i--)
			{
				enemy = Instantiate(enemyPrefab) as GameObject;
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
				enemy = Instantiate(enemyPrefab) as GameObject;
				Vector2 location = locations[i - 1];
				enemy.transform.position = location;
				float angle = Random.Range(0, 360);
				enemy.transform.Rotate(0, 0, angle);
				enemyCount++;
			}
		}
		enemyCount -= 1;

	}
}
