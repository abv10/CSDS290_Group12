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

    private GameObject _dodgeball;
    public Transform enemyThrowPoint;
    public float force = 10f;
    public bool alive;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerCharacter>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            FollowPlayer();

        }
        else
        {
            MoveAwayFromPlayer();
        }
        LookAtPlayer();
        Shoot();
    }

    public void Shoot()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.TransformPoint(Vector3.forward * 1.5f));
        if (hits != null)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                if(hits[i].collider.gameObject.GetComponent<Enemy>() == null)
                {
                    if(hits[i].collider.gameObject.GetComponent<PlayerCharacter>() != null)
                    {
                        if (_dodgeball == null)
                        {
                            ///The ball hits the player (add a throwpoint)
                            _dodgeball = Instantiate(dodgeball) as GameObject;
                            _dodgeball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                            _dodgeball.transform.rotation = transform.rotation;
                            _dodgeball.GetComponent<Rigidbody2D>().AddForce(_dodgeball.transform.up * force, ForceMode2D.Impulse);
                        }
                    }
                    break;
                }
            }    
        }
    }
    public void LookAtPlayer()
    {
        Vector3 direction = target.position - this.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x);
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }
    public void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void MoveAwayFromPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
    }
}
