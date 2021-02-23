using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTime = 100f;
    public GameObject enemyPrefab;
    public bool spawnNext = false;
    public float minTime = 5f;
    public float maxTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        //spawnTime = Random.Range(0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnNext && !GameSceneManager.Instance.gameOver)
        {
            if (GameSceneManager.Instance.Houses.Count == 0)
            {
                GameSceneManager.Instance.gameOver = true;
                GameSceneManager.Instance.levelFailed();
                return;
            }
            EnemyController tempController;
            tempController = Instantiate(enemyPrefab, transform.position, Quaternion.identity).GetComponent<EnemyController>();
            int targetHouseIndex = Random.Range(0, GameSceneManager.Instance.Houses.Count);
            tempController.target = GameSceneManager.Instance.Houses[targetHouseIndex].transform.position;
            tempController.moveTowardTarget = true;
            spawnNext = false;
            spawnTime = Random.Range(minTime, maxTime);
            if (minTime > 1)
            {
                minTime -= 0.1f;
            }
            if (maxTime > 3)
            {
                maxTime -= 0.1f;
            }
        }
        else
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                spawnNext = true;
            }
        }
    }
}
