using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManagerScript : MonoBehaviour
{
    public GameObject gameOverPanel;
    GameObject target;

    public GameObject winPanel;

    public GameObject[] objs;
    public float bossBattleDist;
    public Slider bossHp;
    WeekZone boss;

    //Music manager
    public AudioSource musicSource;
    public AudioClip[] musicClips;

    public bool musicReplay = false;

    public GameObject[] enemy;
    public GameObject closestEnemy;

    float distance;

    int numberOfClip = 3;




    GameObject enemyExample;
    GameObject playerExample;

    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("boss").GetComponent<WeekZone>();
    }

    void Update()
    {


        target = GameObject.FindGameObjectWithTag("Player");
        bossHp.value = boss.hp;



        float dist = Vector3.Distance(target.transform.position , boss.transform.position);
        if (dist < bossBattleDist)
        {
            objs[0].SetActive(true);
            objs[1].SetActive(true);
            objs[2].SetActive(true);
            objs[3].SetActive(true);
            objs[4].SetActive(true);
        }

        if (!target)
        {
            gameOverPanel.SetActive(true);
        }

        if (boss.hp <= 0)
        {
            winPanel.SetActive(true);
        }

        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        
        // music changes
        
        musicManager();

        float eDist = Vector3.Distance(closestEnemy.transform.position , target.transform.position);

        if (eDist < 15 && numberOfClip != 0)
        {
            musicSource.Stop();
            numberOfClip = 0;
            if(!musicSource.isPlaying)
            {
                musicSource.PlayOneShot(musicClips[numberOfClip]);
            }
        }

        if (eDist > 15 && numberOfClip != 3 || !closestEnemy)
        {
            musicSource.Stop();
            numberOfClip = 3;
            if (!musicSource.isPlaying)
            {
                musicSource.PlayOneShot(musicClips[numberOfClip]);
            }
        }

    }

    void musicManager()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.PlayOneShot(musicClips[numberOfClip]);
        }

        

        //closest enemy
        distance = Mathf.Infinity;
        Vector3 position = target.transform.position;

        foreach (GameObject go in enemy)
        {
            Vector3 diff = go.transform.position - position;

            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = go;
                distance = curDistance;
            }
        }
    }

    public void startScene(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            numberOfClip = 0;
        }
    }
}
