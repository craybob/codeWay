using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMelee : MonoBehaviour
{
    public float startTimeAttack;
    private float timeToAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;

    GameObject target;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        timeToAttack = startTimeAttack;

        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        timeToAttack -= Time.deltaTime;
        float dist = Vector3.Distance(transform.position , target.transform.position);


        if (dist < 4)
        {
            anim.SetTrigger("attack");
            Collider2D enemiesToDamage = Physics2D.OverlapCircle(attackPos.position, attackRange);
            enemiesToDamage.gameObject.GetComponent<playerScript>().TakeDamage(damage);
        }
        /*
        if (timeToAttack <= 0 && dist < 3)
        {
            Collider2D enemiesToDamage = Physics2D.OverlapCircle(attackPos.position,attackRange);
            if (enemiesToDamage.gameObject.tag == "Player")
            {
                anim.SetTrigger("attack");
                enemiesToDamage.gameObject.GetComponent<playerScript>().TakeDamage(damage);
                timeToAttack = startTimeAttack;
            }
            
        }
        */
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
