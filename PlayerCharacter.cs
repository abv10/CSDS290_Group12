using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	private int _health;
	private int _streak;
	private bool lose;

	void Start()
	{
		_health = 5;
		_streak = 0;
		lose = false;
	}

	public void Hurt()
	{
		var sceneController = FindObjectOfType<SceneController>();
		if (sceneController.getShieldActive() == false) {
			_health -= 1;
			sceneController.StartCoroutine(sceneController.ShieldActivate(0.25f));
		}
	}

	public void StreakIncrease()
    {
		_streak++;
    }

	public void StreakReset()
    {
		_streak = 0;
    }

	void Update()
	{
		if (_health <= 0 && lose == false)
        {
			lose = true;
			FindObjectOfType<SceneController>().Lose();
		}
	}

	public int getHealth()
	{
		return _health;
	}

	public int getStreak()
	{
		return _streak;
	}

	public void HealthPowerUp()
    {
		_health += 1;
    }
}
