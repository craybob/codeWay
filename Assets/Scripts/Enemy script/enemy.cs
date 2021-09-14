using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    //hp and death
    public int hp;
    public GameObject destroyEffect;

    //following to player and looking to player
    private Rigidbody2D rb;
    Vector2 targetcoord;
    GameObject target;
    NavMeshAgent agent;

    //Shooting
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed;

    public float attackSpeed;
    float attack;

    public GameObject[] dropThing;

    private Animator anim;


    void Start()
    {
        attack = attackSpeed;

        rb = GetComponent<Rigidbody2D>();

        target = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position,target.transform.position);

        if (distance <= 10)
        {
            agent.SetDestination(target.transform.position);
            targetcoord = new Vector2(target.transform.position.x, target.transform.position.y);

            if (attack <= 0)
            {
                Eshoot();
            }
            else
            {
                anim.SetBool("attack", false);
                attack -= Time.deltaTime;
            }
        }



        death();
    }

    void FixedUpdate()
    {
        Vector2 lookdir = targetcoord - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
    }

    void death()
    {
        if (hp <= 0)
        {
            int randomThing = Random.RandomRange(0,dropThing.Length);
            Instantiate(dropThing[randomThing],transform.position,Quaternion.identity);
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Eshoot()
    {
        anim.SetBool("attack", true);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed, ForceMode2D.Impulse);

        attack = attackSpeed;
    }
}
