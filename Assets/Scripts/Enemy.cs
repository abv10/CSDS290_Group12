using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private GameObject dodgeball;
    [SerializeField] private float minRange;
    [SerializeField] private float maxRange;
    public float coolDownTime = 2f;
    private GameObject _dodgeball;
    public Transform enemyThrowPoint;
    public float force = 10f;
    public bool alive;
    private bool coolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerCharacter>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();
            Shoot();

        }
        else if (Vector3.Distance(target.position, transform.position) <= minRange)
        {
            MoveAwayFromPlayer();
        }
        else
        {
            FollowPlayer();
        }
        LookAtPlayer();
    }

    public void Shoot()
    {
        if (!coolDown)
        {
            ///The ball hits the player (add a throwpoint)
            _dodgeball = Instantiate(dodgeball, enemyThrowPoint.position, enemyThrowPoint.rotation);
            _dodgeball.GetComponent<Rigidbody2D>().AddForce(enemyThrowPoint.up * force, ForceMode2D.Impulse);
            coolDown = true;
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);
        coolDown = false;
    }
    public void LookAtPlayer()
    {
        Vector3 direction = target.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg + 90f);
    }
    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void MoveAwayFromPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, -speed * Time.deltaTime*0);
    }
}