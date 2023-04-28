using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] GameObject player;
    [SerializeField] float walkPointCushion;
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] Vector2 maxMinWaitTimes;

    Transform walkPoint;

    float distance, deadTimer;

    int walkPointCount;

    bool attacking = false;
    bool thinking = false;
    bool waiting = false;
    bool dead;
    public SpriteRenderer SR;
    public CircleCollider2D CC;
    public ParticleSystem bloodBurst;

    private void Start()
    {
        enemyController = FindObjectOfType<EnemyController>();
        int randomPoint = Random.Range(0, enemyController.WalkPoints.Length);
        walkPoint = enemyController.WalkPoints[randomPoint];
        distance = Vector2.Distance(gameObject.transform.position, walkPoint.position);
        deadTimer = 3;
    }

    private void Update()
    {
        if (!thinking)
          StartCoroutine(Attack());

        if (!attacking && !waiting)
        {
            distance = Vector2.Distance(gameObject.transform.position, walkPoint.position);

            if (distance < walkPointCushion)
            {
                StartCoroutine(NewWalkpoint());
            }

            gameObject.transform.position = Vector2.MoveTowards(this.transform.position, walkPoint.transform.position, (moveSpeed / 10) * Time.deltaTime);
        }
        else if (attacking && !waiting)
        {
            StartCoroutine(StopAttacking());
            distance = Vector2.Distance(gameObject.transform.position, player.transform.position);

            if (distance < walkPointCushion)
            {
                StartCoroutine(NewWalkpoint());
            }

            gameObject.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (moveSpeed / 10) * Time.deltaTime);
        }
        if(dead == true)
        {
            deadTimer -= Time.deltaTime;
            if(deadTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator NewWalkpoint()
    {
        waiting = true;
        float randomTime = Random.Range(maxMinWaitTimes.y / 4, maxMinWaitTimes.x / 4);
        yield return new WaitForSeconds(randomTime);

        int randomPoint = Random.Range(0, enemyController.WalkPoints.Length);
        walkPoint = enemyController.WalkPoints[randomPoint];
        waiting = false;
    }

    IEnumerator Attack()
    {
        thinking = true;
        float randomTime = Random.Range(maxMinWaitTimes.y, maxMinWaitTimes.x);

        yield return new WaitForSeconds(randomTime);

        attacking = true;
    }

    IEnumerator StopAttacking()
    {
        thinking = true;
        float randomTime = Random.Range(maxMinWaitTimes.y / 2, maxMinWaitTimes.x / 2);

        yield return new WaitForSeconds(randomTime);

        attacking = false;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player Projectile")
        {
            SR.enabled = false;
            CC.enabled = false;
            bloodBurst.Play();
            dead = true;
            Destroy(collision.gameObject);
        }
    }
}
