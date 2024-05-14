using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
GameObject[] spawnPoint;
public GameObject Zombie;
public float minSpawnTime = 2f;
public float maxSpawnTime = 5f;
public float lastSpawnTime = 10;
public float spawnTime = 0;

    void Start()
    {
        spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        UpdateSpawnTime();
    }

        void Update()
    {
        if (Time.time >= lastSpawnTime + spawnTime)
            Spawn();
    }

     void UpdateSpawnTime()
    {
        lastSpawnTime = Time.time;
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Spawn()
    {
        int point = Random.Range(0, spawnPoint.Length);
        Instantiate(Zombie, spawnPoint[point].transform.position, Quaternion.identity);
        UpdateSpawnTime();
    }
}