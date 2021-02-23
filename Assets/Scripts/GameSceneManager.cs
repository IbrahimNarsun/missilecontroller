using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    public GameObject missilePrefab;
    public GameObject explosionEffectPrefab;
    public GameObject flashPrefab;
    public GameObject ScorePrefab;
    public GameObject[] missileSpawnPoints;
    public HudManager hud;
    public List<GameObject> Houses;
    public bool gameOver = false;
    public int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        hud.updateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // Construct a ray from the current touch coordinates
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Create a particle if hit
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "BG")
                {
                    GameObject spawnedMissile;
                    spawnedMissile = Instantiate(missilePrefab, missileSpawnPoints[findClosestLauncher(hit.point)].transform.position, Quaternion.identity);
                    //spawnedMissile.GetComponent<MissileController>().target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
                    spawnedMissile.GetComponent<MissileController>().target = hit.point;
                    spawnedMissile.GetComponent<MissileController>().moveTowardTarget = true;
                }
            }
        }
    }

    public int findClosestLauncher(Vector2 touchPosition)
    {
        float minDistance = 100000;
        int closestLauncherIndex = 0;
        for (int i = 0; i < missileSpawnPoints.Length; i++)
        {
            if (missileSpawnPoints[i] == null)
            {
                continue;
            }
            float tempDistance = Vector2.Distance(touchPosition, missileSpawnPoints[i].transform.position);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                closestLauncherIndex = i; 
            }
        }
        return closestLauncherIndex;
    }

    public void destroyHouse(HouseData house)
    {
        foreach (GameObject temp in Houses)
        {
            if (temp.GetComponent<HouseData>() == house)
            {
                Instantiate(explosionEffectPrefab, house.transform.position, Quaternion.identity);
                GameObject temp2;
                temp2 = temp;
                Houses.Remove(temp);
                Destroy(temp2);
                break;
            }
        }
    }

    public void resetScene()
    {
        SceneManager.LoadScene(0);
    }

    public void addPoints(int points)
    {
        score += points;
        hud.updateScore();
    }

    public void levelFailed()
    {
        hud.levelFail();
    }
}
