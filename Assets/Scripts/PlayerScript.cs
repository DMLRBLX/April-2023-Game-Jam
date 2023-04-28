using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed;
    public Vector3 movementDirection, shootDirection;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject bullet, gun;
    public float shootTimer, shootTimerMax, hitTimer, hitTimerMax;
    public bool suction;
    public GameObject suctionOrigin;
    public float suckForce;
    public float gravityModifier;
    public AudioSource shootSound, hitSound;
    public float health, healthMax;
    public Image healthBar;
    public GameObject endScreen;
    public TextMeshProUGUI endText;
    // Start is called before the first frame update
    void Start()
    {
        health = healthMax;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementDirection.x = Input.GetAxis("Move Horizontal");
        movementDirection.y = Input.GetAxis("Move Vertical");
        shootDirection.x = Input.GetAxis("Shoot Horizontal");
        shootDirection.y = Input.GetAxis("Shoot Vertical");

        rb.AddForce(movementDirection * movementSpeed);
        if(movementDirection.magnitude > .2f)
        {
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
        if (shootDirection.magnitude > .2f)
        {
            anim.SetBool("Aiming", true);
            transform.up = Vector2.Lerp(transform.up,-shootDirection, .5f);
        }
        else
        {
            anim.SetBool("Aiming", false);
            transform.up = Vector2.Lerp(transform.up, -movementDirection, .5f);
        }
        shootTimer -= Time.deltaTime;
        if(suction == true)
        {
            Vector3 suckDirection = suctionOrigin.transform.position - transform.position;
            rb.AddForce(suckDirection.normalized * suckForce);
        }
        hitTimer -= Time.deltaTime;
        healthBar.fillAmount = health / healthMax;
        if(health <= 0)
        {
                endText.text = "Exterminated";
                endScreen.SetActive(true);
                Time.timeScale = 0;
            
        }
    }
    public void Update()
    {
        if (shootDirection.magnitude > .2f)
        {
            if (Input.GetKeyDown(KeyCode.Space) && shootTimer < 0)
            {
                anim.SetTrigger("Fire");
                GameObject newBullet = Instantiate(bullet, gun.transform.position, transform.rotation);
                newBullet.transform.up = -gun.transform.up;
                newBullet.GetComponent<ProjectileScript>().gravityModifier = gravityModifier;
                rb.AddForce(transform.up * 100);
                shootSound.Play();
                shootTimer = shootTimerMax;
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Suction Zone")
        {
            suction = true;
            suctionOrigin = collision.gameObject.transform.parent.gameObject;
        }
        if(collision.gameObject.name == "Water Puddle")
        {
            rb.drag = 2;
        }
        if (collision.gameObject.name == "Goo Puddle")
        {
            movementSpeed = 3;
        }
        if(collision.gameObject.tag == "Enemy" && hitTimer < 0)
        {
            hitSound.Play();
            hitTimer = hitTimerMax;
            health -= 1;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Suction Zone")
        {
            suction = false;
            suctionOrigin = null;
        }
        if (collision.gameObject.name == "Water Puddle")
        {
            rb.drag = 4;
        }
        if (collision.gameObject.name == "Goo Puddle")
        {
            movementSpeed = 7;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && hitTimer < 0)
        {
            hitSound.Play();
            hitTimer = hitTimerMax;
            health -= 1;
            anim.SetTrigger("Hit");
        }
    }
    public void RetryLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
