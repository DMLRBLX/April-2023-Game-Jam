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
    [Header("Transformer Variables")]
    public SpriteRenderer blackOut;
    public float blackOutTimer, blackOutTimerMax, blackOutDuration, blackOutDurationMax;
    public GameObject sparks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
        }
    }
}
