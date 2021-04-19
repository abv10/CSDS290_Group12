using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float speed;
    [SerializeField] private GameObject dodgeball;
    private GameObject _dodgeball;
    public Transform enemyThrowPoint;
    public float force = 10f;
    public bool alive;
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            transform.eulerAngles = new Vector3(0, 0, -transform.eulerAngles.z);
            transform.LookAt(target.transform.position, transform.up);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);



            if (Vector3.Distance(transform.position, target.transform.position) > 0.5f)
            {
                transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
                transform.Rotate(new Vector3(0, 0, 90));
            }

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);
            if (hit)
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerMovement>() != null)
                {
                    if (_dodgeball == null || _dodgeball != null)
                    {
                        _dodgeball = Instantiate(dodgeball, enemyThrowPoint.position, enemyThrowPoint.rotation);
                        Rigidbody2D rb = _dodgeball.GetComponent<Rigidbody2D>();
                        rb.AddForce(enemyThrowPoint.up * force, ForceMode2D.Impulse);
                    }

                }
            }
        }
        else
        {
            GameObject.Destroy(this);
        }
    }



}
