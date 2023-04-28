using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public EnemyController controller;
    public float spawnTimer, spawnTimerMax;
    public float timer, minutes, seconds;
    public TextMeshProUGUI timerText;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTimerMax/2;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer <= 0)
        {
            Instantiate(enemy, controller.WalkPoints[Random.Range(0, controller.WalkPoints.Length)].position, transform.rotation);
            if(minutes >= 2)
            {
                Instantiate(enemy, controller.WalkPoints[Random.Range(0, controller.WalkPoints.Length)].position, transform.rotation);
            }
            spawnTimer = spawnTimerMax;
        }
        timer += Time.deltaTime;
        seconds += Time.deltaTime;
        if(Mathf.RoundToInt(seconds) >= 60)
        {
            minutes += 1;
            spawnTimerMax -= .5f;
            seconds = 0;
        }
        if (Mathf.RoundToInt(seconds) >= 10)
        {
            timerText.text = minutes + ":" + Mathf.RoundToInt(seconds);
        }
        else
        {
            timerText.text = minutes + ":0" + Mathf.RoundToInt(seconds);
        }
    }
}
