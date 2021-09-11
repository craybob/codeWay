using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;

    void Start()
    {
        Invoke("destroy", 3f);
    }

    void destroy()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerScript>().TakeDamage(damage);
            destroy();
        }

        else if (collision.gameObject.tag == "other")
        {
            destroy();
        }
    }
}
