using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    private Animator playerAnim;

    public AudioSource effectSound;
    public AudioClip[] audioClip;
    public AudioSource musicSource;

    public int ammo;
    bool attack = true;
    public int magazine = 24;


    void Start()
    {
        playerAnim = GetComponent<Animator>();
        musicSource.PlayOneShot(audioClip[4]);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (ammo != 0 && attack )
            {
                Shoot();
            }
            else if (ammo <= 0)
            {
                attack = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            effectSound.PlayOneShot(audioClip[1]);
            Invoke("Reload", 2f);
        }
    }

    void Reload()
    {
        if (magazine > 0)
        {
            ammo = 12;
            magazine--;
        }

        else if (magazine <= 0)
        {
            ammo += 0;
        }

        attack = true;
    }

    void Shoot()
    {
        effectSound.PlayOneShot(audioClip[0]);
        playerAnim.SetTrigger("attack");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed , ForceMode2D.Impulse);

        ammo -= 1;
    }



}
