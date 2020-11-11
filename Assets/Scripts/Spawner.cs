using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public wave[] waves;
    public Enemy enemy;

    wave currentWave;
    int currentWaveNum;

    int toSpawn;
    int enemiesRemaining;
    float nextSpawnTimer;

    public void Start()
    {
        NextWave();
    }

    public void Update()
    {
        if(toSpawn > 0 && Time.time > nextSpawnTimer)
        {
            toSpawn--;
            nextSpawnTimer = Time.time + currentWave.timeBetweenSpawns;

            Enemy spawnedEnemy = Instantiate(enemy, gameObject.transform.position, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    public void OnEnemyDeath()
    {
        enemiesRemaining--;

        if(enemiesRemaining == 0)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        currentWaveNum++;
        if(currentWaveNum -1 < waves.Length)
        {
            currentWave = waves[currentWaveNum - 1];
            toSpawn = currentWave.enemyCount;
            enemiesRemaining = toSpawn;
        }

    }

    [System.Serializable]
    public class wave
    {
        public int enemyCount;
        public float timeBetweenSpawns;
    }
}
