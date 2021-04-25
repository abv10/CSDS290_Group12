using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 5.0f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;
    private bool dash = false;
    [SerializeField] private LayerMask dashLayerMask;
    private Vector2 moveDir;
    [SerializeField] private ParticleSystem dasheffect1;
    [SerializeField] private ParticleSystem dasheffect2;
    [SerializeField] private Transform dasheff2;

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            dash = true;
        }

        moveDir = new Vector2(movement.x, movement.y).normalized;

    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 270f;
        rb.rotation = angle;

        if (dash)
        {
            float dashAmount = 1.0f;
            RaycastHit2D raycastHit2D;
            Vector2 dashPosition;
            if (moveDir.x == 0 && moveDir.y == 0)
            {
                dashPosition = (Vector2)transform.position + lookDir.normalized * dashAmount;
                raycastHit2D = Physics2D.Raycast(transform.position, lookDir.normalized, dashAmount, dashLayerMask);
                dasheffect1.Play();
            }
            else
            {
                dashPosition = (Vector2)transform.position + moveDir * dashAmount;
                raycastHit2D = Physics2D.Raycast(transform.position, moveDir, dashAmount, dashLayerMask);
                angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg - 270f;
                dasheff2.position = new Vector3(dashPosition.x, dashPosition.y, 0);
                dasheff2.rotation = Quaternion.Euler(0, 0, angle);
                dasheffect2.Play();
            }

            if (raycastHit2D.collider != null)
            {
                dashPosition = raycastHit2D.point;
            }

            rb.MovePosition(dashPosition);
            dash = false;
        }
    }

}