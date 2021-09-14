using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    // Movement
    public float speed;
    private Vector2 moveVelocity;
    private Rigidbody2D rb;
    
    //HP and death
    public int hp;
    public GameObject destroyEffect;
    bool alive = true;
    
    //Indicators
    public Text hpText;
    public Text ammoText;
    
    //Looking to mouse
    public Camera cam;
    Vector2 mousePos;
    playerShoot shootScript;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootScript = GetComponent<playerShoot>();

        Time.timeScale = 1f;
    }


    void Update()
    {
        //Move direction
        Vector2 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;
        //mouse direction
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


        destroy();
        Indicators();
    }

    void FixedUpdate()
    {
        //movement with physics
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);
        //looking to mouse direction
        Vector2 lookdir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void destroy()
    {
        if (hp <= 0 && alive == true)
        {
            Death();
        }
    }

    void Death()
    {
        shootScript.effectSound.PlayOneShot(shootScript.audioClip[2]);

        BoxCollider2D col = GetComponent<BoxCollider2D>();
        SpriteRenderer sprt = GetComponent<SpriteRenderer>();
        playerScript thisScript = GetComponent<playerScript>();
        shootScript.musicSource.Stop();
        shootScript.musicSource.PlayOneShot(shootScript.audioClip[5]);
        thisScript.enabled = false;
        col.enabled = false;
        sprt.enabled = false;
        

        Invoke("GameOver",1f);
        
        alive = false;
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        shootScript.enabled = false;
        Destroy(gameObject);
    }

    void Indicators()
    {
        hpText.text = ("HP" + hp);
        ammoText.text = ("Ammo" + shootScript.ammo + "/" + shootScript.magazine);
    }

    public void TakeDamage(int Damage)
    {
        shootScript.effectSound.PlayOneShot(shootScript.audioClip[3]);
        hp -= Damage;
    }
}
