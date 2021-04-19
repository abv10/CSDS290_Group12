using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
	[SerializeField] private GameObject enemyPrefab;
	private GameObject enemy;
	public static int enemyCount = 0;
	private int wave = 0;

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
				Vector2 location = new Vector2(Random.Range(-5.8f, 5.8f), Random.Range(-4.2f, 4.2f));
				enemy.transform.position = location;
				while (enemy.GetComponent<Collider>() != null)
				{
					location = new Vector2(Random.Range(-5.8f, 5.8f), Random.Range(-4.2f, 4.2f));
					enemy.transform.position = location;
				}
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
				Vector2 location = new Vector2(Random.Range(-5.8f, 5.8f), Random.Range(-4.2f, 4.2f));
				enemy.transform.position = location;
				while (enemy.GetComponent<Collider>() != null)
				{
					location = new Vector2(Random.Range(-5.8f, 5.8f), Random.Range(-4.2f, 4.2f));
					enemy.transform.position = location;
				}
				float angle = Random.Range(0, 360);
				enemy.transform.Rotate(0, 0, angle);
				enemyCount++;
			}
		}
		enemyCount -= 1;

	}

}
