using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerShoot : MonoBehaviour
{
    //Shooting
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;

    private Animator playerAnim;

    public int weapons;
    //Audio
    public AudioSource effectSound;
    public AudioClip[] audioClip;
    public AudioSource musicSource;
    //Reload
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
        changeWeapons();
        if (weapons == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (ammo != 0 && attack)
                {
                    playerAnim.SetTrigger("attack");
                    Shoot();
                }
                else if (ammo <= 0)
                {
                    attack = false;
                }
            }
        }
        if (weapons == 1)
        {
            if (Input.GetButton("Fire1"))
            {
                if (ammo != 0 && attack)
                {
                    playerAnim.SetTrigger("gunAttack");
                    Shoot();
                }
                else if (ammo <= 0)
                {
                    attack = false;
                }
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

    void changeWeapons()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weapons = 0;
            playerAnim.SetInteger("Weapons", weapons);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weapons = 1;
            playerAnim.SetInteger("Weapons", weapons);
        }
    }

    void Shoot()
    {
        effectSound.PlayOneShot(audioClip[0]);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletSpeed , ForceMode2D.Impulse);

        ammo -= 1;
    }



}
