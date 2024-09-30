//made by Laura Unverzagt
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmenyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();
    public  List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        StartMenu.Instance.BeginGame.AddListener(SetUp);
        StartMenu.Instance.EndGame.AddListener(ShutDown);
    }

    private void SetUp()
    {
        InvokeRepeating("SpawnEnemy", 3f, 4f);
    }
    private void ShutDown()
    {
        foreach (GameObject en in enemies) { Destroy(en); }
        enemies = new List<GameObject>();
        CancelInvoke("SpawnEnemy");
    }

    private void SpawnEnemy()
    {
        // checks whether all spawns are full
        if (enemies.Count >= spawnPoints.Count) return;

        int randomIndex = 0;
        bool isSpawnFree = false;

        // finds random point for new enemy
        while (isSpawnFree == false)
        {
            randomIndex = Random.Range(0, spawnPoints.Count);
            isSpawnFree = true;
            foreach (GameObject en in enemies)
            {
                // if the spawn point doesn't have an enemy, continue
                if (en.transform.position == spawnPoints[randomIndex].transform.position)
                {
                    isSpawnFree = false;
                    break; 
                }
            }     
        }
        
        // spawns new enemy
        Vector3 pos = spawnPoints[randomIndex].transform.position;
        GameObject b = Instantiate(enemyPrefab, pos, Quaternion.LookRotation(new Vector3(-pos.x, 0, -pos.z)));
        b.GetComponent<Enemy>().getEnemyManager(this);
        enemies.Add(b);
    }
}
