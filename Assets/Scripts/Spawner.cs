using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public wave[] waves;
    public Enemy enemy;

    public GameObject[] spawners;

    wave currentWave;
    int currentWaveNum;

    public UIManager manager;

    int toSpawn;

    int enemiesRemaining;
    float nextSpawnTimer;

    public void Update()
    {
        if(toSpawn > 0 || currentWave.infiniteMode && Time.time > nextSpawnTimer)
        {
            toSpawn--;
            nextSpawnTimer = Time.time + currentWave.timeBetweenSpawns;

            int whereToSpawn = Random.Range(0,spawners.Length);

            Enemy spawnedEnemy = Instantiate(enemy, spawners[whereToSpawn].transform.position, Quaternion.identity) as Enemy;
            spawnedEnemy.OnDeath += OnEnemyDeath;
            spawnedEnemy.SetCharacteristics(currentWave.hitsToKill, currentWave.enemyHealth);
        }
    }

    public void OnEnemyDeath()
    {
        enemiesRemaining--;

        manager.UpdateZombiesLeft(enemiesRemaining);

        if(enemiesRemaining == 0)
        {
            manager.EnableDaysScreen();
            manager.SetDayNumber(currentWaveNum + 1);
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
            manager.UpdateZombiesLeft(enemiesRemaining);
        }

    }

    [System.Serializable]
    public class wave
    {
        public bool infiniteMode;
        public int enemyCount;
        public float timeBetweenSpawns;

        public int hitsToKill;
        public float enemyHealth;
    }
}
