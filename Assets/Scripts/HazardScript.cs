using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    public bool broken;
    public string hazardType;
    public SpriteRenderer SR;
    public Sprite brokenSprite;
    [Header ("Window Variables")]
    public GameObject suctionZone;
    public ParticleSystem glassBreak;
    [Header("Transformer Variables")]
    public SpriteRenderer blackOut;
    public float blackOutTimer, blackOutTimerMax, blackOutDuration, blackOutDurationMax;
    public GameObject sparks;
    [Header("Pipes Variables")]
    public GameObject puddle;
    public GameObject drops;
    [Header("Fuel Tank Variables")]
    public ParticleSystem fireColumn;
    public float fireTimer, fireTimerMax, fireDuration, fireDurationMax;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(broken == true && hazardType == "Transformer")
        {
            blackOutTimer -= Time.deltaTime;
            if(blackOutTimer <= 0)
            {
                blackOut.enabled = true;
                blackOutTimer = blackOutTimerMax + Random.Range(-5f, 5f);
            }
            if (blackOut.enabled == true)
            {
                blackOutDuration -= Time.deltaTime;
                if(blackOutDuration <= 0)
                {
                    blackOut.enabled = false;
                    blackOutDuration = blackOutDurationMax + Random.Range(-1f, 1f);
                }
            }
        }
        if(broken == true && hazardType == "Pipes")
        {
            puddle.SetActive(true);
            drops.SetActive(true);
            puddle.transform.localScale = Vector2.MoveTowards(puddle.transform.localScale, new Vector2(1.5f, 1.5f), .01f);
        }
        if (broken == true && hazardType == "Fuel Tank")
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0)
            {
                fireColumn.Play();
                fireTimer = fireTimerMax;
                fireDuration = fireDurationMax;
                fireColumn.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            }
            if (fireColumn.isStopped == false)
            {
                fireDuration -= Time.deltaTime;
                if (fireDuration <= 0)
                {
                    fireColumn.Stop();
                    fireDuration = fireDurationMax;
                    fireColumn.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Projectile" && broken == false)
        {
            broken = true;
            SR.sprite = brokenSprite;
            Destroy(collision.gameObject);
            if(hazardType == "Window")
            {
                suctionZone.SetActive(true);
                glassBreak.Play();
            }
            if(hazardType == "Transformer")
            {
                sparks.SetActive(true);
            }
            if (hazardType == "Computer")
            {
                sparks.SetActive(true);
                FindObjectOfType<PlayerScript>().gravityModifier = .15f;
            }
            if(hazardType == "Pipes")
            {
                glassBreak.Play();
            }
            if (hazardType == "Fuel Tank")
            {
                fireColumn.gameObject.SetActive(true);
            }
        }
        if (collision.gameObject.tag == "Player Projectile" && broken == true)
        {
            Destroy(collision.gameObject);
        }
    }
}
