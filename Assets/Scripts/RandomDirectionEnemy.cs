using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDirectionEnemy : MonoBehaviour
{
    public float speed = 2.0f;
    private GameObject player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float movementTime;

    public Transform enemyThrowPoint;
    public GameObject ballPrefab;
    public float force = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        movement = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
        movement.Normalize();
        movementTime = Random.Range(1.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        rb.rotation = angle;
        if (movementTime <= 0)
        {
            movement = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f));
            movement.Normalize();
            movementTime = Random.Range(1.0f, 3.0f);
            Throw();
        }
        movementTime -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        movement.x = -movement.x;
        movement.y = -movement.y;
    }

    public void Throw()
    {
        GameObject ball = Instantiate(ballPrefab, enemyThrowPoint.position, enemyThrowPoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(enemyThrowPoint.up * force, ForceMode2D.Impulse);
    }
    
     public void increaseSpeed(int speedToAdd)
    {
        if (speedToAdd < 11)
        {
            if (speedToAdd < 6)
            {
                for (int i = 0; i < speedToAdd; i++)
                {
                    speed *= 1.2f;
                }
            }

            else
            {
                for (int i = 0; i < 5; i++)
                {
                    speed *= 1.2f;
                }
                for (int i = 0; i < (speedToAdd - 5); i++)
                {
                    speed *= 1.1f;
                }
            }
        }
    }
    public void increaseForce(int forceToAdd)
    {
        if (forceToAdd < 11)
        {
            if (forceToAdd < 6)
            {
                for (int i = 0; i < forceToAdd; i++)
                {
                    force *= 1.2f;
                }
            }

            else
            {
                for (int i = 0; i < 5; i++)
                {
                   force *= 1.1f;
                }
                for (int i = 0; i < (forceToAdd - 5); i++)
                {
                    force *= 1.05f;
                }
            }
        }
    }

}
