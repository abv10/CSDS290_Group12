using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDodgeBall : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject hitObject = collision.gameObject;
        EnemyDeath enemy = hitObject.GetComponent<EnemyDeath>();
        Dodgeball dodgeball = hitObject.GetComponent<Dodgeball>();
        ShieldPowerUp shield = hitObject.GetComponent<ShieldPowerUp>();

        if (hitObject == GameObject.Find("Player"))
        {
            PlayerCharacter p = hitObject.GetComponent<PlayerCharacter>();
            p.Hurt();
            Destroy(this.gameObject);
        }
        //I don't know if we want the enemy to be destroyed by its own dodgeballs
        else if (enemy != null)
        {
            //enemy.Die();
            Destroy(this.gameObject);
        }
        else if (shield != null)
        {
            Destroy(this.gameObject);
        }
        /* else if (dodgeball != null)
        {
            var speed = lastVelocity.magnitude; //We can reduce this if we want
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * speed;
        } */    //cool idea but causes ball interaction glitches
        else
        {
            Destroy(this.gameObject);
        }

    }

}