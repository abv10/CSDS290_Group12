using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitObject = collision.gameObject;

        if (hitObject == GameObject.Find("Player"))
        {
            PlayerCharacter p = hitObject.GetComponent<PlayerCharacter>();
            FindObjectOfType<PlayerCharacter>().HealthPowerUp();
            Destroy(this.gameObject);
        }
    }

}
