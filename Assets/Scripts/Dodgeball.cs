using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodgeball : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 lastVelocity;
    private bool firstWall;

    void Start()
    {
        firstWall = true;
        rb = this.GetComponent<Rigidbody2D>();
    }

    //Not sure if start or Awake is the best when initializing a ball through
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Throw(Vector2 direction)
    {
        rb.AddForce(direction);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;

        if(rb.velocity.magnitude <= 2.5)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject hitObject = collision.gameObject;
        EnemyDeath enemy = hitObject.GetComponent<EnemyDeath>();
        ShieldPowerUp shield = hitObject.GetComponent<ShieldPowerUp>();

        if (hitObject == GameObject.Find("Player"))
        {
            PlayerCharacter p = hitObject.GetComponent<PlayerCharacter>();
            p.Hurt();
            Destroy(this.gameObject);
        }
        else if (enemy != null)
        {
            EnemyDeath e = hitObject.GetComponent<EnemyDeath>();
            e.Die();
            Destroy(this.gameObject);
            FindObjectOfType<PlayerCharacter>().StreakIncrease();
        }
        else if (shield != null && firstWall == false)
        {
            Destroy(this.gameObject);
        }
        else if (shield != null && firstWall == true)
        {
        }
        else
        {
            var speed = lastVelocity.magnitude; //We can reduce this if we want
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = (direction * speed) * 0.95f;
            
            if (firstWall == true)
            {
                FindObjectOfType<PlayerCharacter>().StreakReset();
                firstWall = false;
            }
        }

    }

}