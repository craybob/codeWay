using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekZone : MonoBehaviour
{
    public int hp;
    public GameObject boss;

    void Update()
    {

        if (hp <= 0)
        {

            Destroy(boss);
        }
    }

    public void TakeDamage(int Damage)
    {
        hp -= Damage;
    }
}
