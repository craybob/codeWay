using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public float spawnTime = 8f;

    private int numOfEnemy = 1;
    void Start()
    {
        InvokeRepeating("EnemySpawn", 3f, spawnTime);
    }

    void EnemySpawn()
    {
        for (int i = 0; i < numOfEnemy; i++)
        {
            int randomEnemy = Random.RandomRange(0 , enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemy], transform.position, Quaternion.identity);
        }
        numOfEnemy ++;
    }
}
