using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossScript : MonoBehaviour
{
    // following
    GameObject target;
    NavMeshAgent agent;

    // looking to player and super attack
    Rigidbody2D rb;
    Vector2 targetcoord;
    public float chargeSpeed;
    public Transform firePoint;

    //super attack reload
    public float chargeReSpeed;
    float reloadSpeed;

    public int damage;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {

        reloadSpeed -= Time.deltaTime;
        if (reloadSpeed >= 0) 
        { 
            targetcoord = new Vector2(target.transform.position.x, target.transform.position.y); 
        }
    }
    void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);

        
        if (distance <= 30 && reloadSpeed >= 0)
        {
            agent.SetDestination(target.transform.position);

            Vector2 lookdir = targetcoord - rb.position;
            float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
        }

        if (reloadSpeed <= 0)
        {
            rb.AddForce(firePoint.up * chargeSpeed, ForceMode2D.Impulse);
            Invoke("Charge", 5f);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerScript>().TakeDamage(damage);
        }
    }

    void Charge()
    {
        reloadSpeed = chargeReSpeed;
    }
}
