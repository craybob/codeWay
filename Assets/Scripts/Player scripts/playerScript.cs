using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{
    public float speed;

    public int hp;
    public Text hpText;
    public Text ammoText;
    

    private Vector2 moveVelocity;
    private Rigidbody2D rb;

    public Camera cam;

    Vector2 mousePos;

    public GameObject destroyEffect;

    playerShoot shootScript;

    bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shootScript = GetComponent<playerShoot>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (hp <= 0 && alive == true)
        {
            Death();
        }

        Indicators();
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.deltaTime);

        Vector2 lookdir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
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
