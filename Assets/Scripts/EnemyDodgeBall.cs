﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDodgeBall : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 lastVelocity;

    public static bool waveOver = false;

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

        if (waveOver)
        {
            StartCoroutine(ClearBalls());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        GameObject hitObject = collision.gameObject;
        EnemyDeath enemy = hitObject.GetComponent<EnemyDeath>();

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
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public IEnumerator ClearBalls()
    {
        waveOver = false;
        yield return new WaitForSeconds(5.0f);
        //Destroy all balls
    }

}
