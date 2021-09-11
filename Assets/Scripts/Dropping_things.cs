using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropping_things : MonoBehaviour
{
    public int typeOfThing;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (typeOfThing == 1)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<playerScript>().hp += 1;
                Destroy(gameObject);
            }
        }
        if (typeOfThing == 2)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<playerShoot>().magazine += 1;
                Destroy(gameObject);
            }
        }
    }
}
