using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{

	public void Die()
    {
        Destroy(this.gameObject);
        FindObjectOfType<SceneController>().DecreaseEnemyCount();
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
