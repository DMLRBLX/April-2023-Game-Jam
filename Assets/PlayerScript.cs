using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float movementSpeed;
    public Vector3 movementDirection, shootDirection;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject bullet, gun;
    public float hitTimer, hitTimerMax;
    // Start is called before the first frame update
    void Start()
    {
        
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Fire");
                GameObject newBullet = Instantiate(bullet, gun.transform.position, transform.rotation);
                newBullet.transform.up = -gun.transform.up;
            }
        }
        else
        {
            anim.SetBool("Aiming", false);
            transform.up = Vector2.Lerp(transform.up, -movementDirection, .5f);
        }
        hitTimer -= Time.deltaTime;
    }
    public void Update()
    {
        if (shootDirection.magnitude > .2f)
        {
            if (Input.GetKeyDown(KeyCode.Space) && hitTimer < 0)
            {
                anim.SetTrigger("Fire");
                GameObject newBullet = Instantiate(bullet, gun.transform.position, transform.rotation);
                newBullet.transform.up = -gun.transform.up;
                rb.AddForce(transform.up * 100);
                hitTimer = hitTimerMax;
            }
        }
    }
}
