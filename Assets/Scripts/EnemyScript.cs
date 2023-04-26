using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;
    [SerializeField] GameObject player;
    [SerializeField] float walkPointCushion;
    [SerializeField] float moveSpeed = 15f;

    Transform walkPoint;

    float distance;

    private void Update()
    {
        if (walkPoint == null | distance <= walkPointCushion)
        {
            int randomPoint = Random.Range(0, enemyController.WalkPoints.Length);
            walkPoint = enemyController.WalkPoints[randomPoint];
            distance = Vector2.Distance(gameObject.transform.position, walkPoint.position);
        }

        Vector2 dir = player.transform.position - gameObject.transform.position;

        dir.Normalize();
        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        gameObject.transform.rotation = Quaternion.Euler(Vector3.forward * ang);
        gameObject.transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, (moveSpeed / 10) * Time.deltaTime);
    }
}
