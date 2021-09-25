using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public int damage;

    public GameObject weekZone;

    void Start()
    {
        Invoke("destroy" , 3f);
        
    }

    void destroy()
    {
        Destroy(gameObject);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<enemy>().TakeDamage(damage);
            destroy();
        }
        if (collision.gameObject.tag == "boss")
        {
            collision.gameObject.GetComponent<WeekZone>().TakeDamage(damage);
            destroy();
        }

        else if (collision.gameObject.tag == "other")
        {
            destroy();
        }
    }
}
