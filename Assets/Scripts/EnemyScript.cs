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

    float distance;

    int walkPointCount;

    bool attacking = false;
    bool thinking = false;
    bool waiting = false;

    private void Start()
    {
        int randomPoint = Random.Range(0, enemyController.WalkPoints.Length);
        walkPoint = enemyController.WalkPoints[randomPoint];
        distance = Vector2.Distance(gameObject.transform.position, walkPoint.position);
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
}
