using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThrowing : MonoBehaviour
{

    public Transform throwPoint;
    public GameObject ballPrefab;

    public float force = 20f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Throw();
        }
    }

    void Throw()
    {
        GameObject ball = Instantiate(ballPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.AddForce(throwPoint.up * force, ForceMode2D.Impulse);
    }

}
