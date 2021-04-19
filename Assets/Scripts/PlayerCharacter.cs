using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
	private int _health;

	void Start()
	{
		_health = 5;
	}

	public void Hurt()
	{
		_health -= 1;
		Debug.Log("Health: " + _health);
	}

	void Update()
	{

	}

	public int getHealth()
	{
		return _health;
	}
}
