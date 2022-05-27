using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //wave enum
    public enum SpawnState {SPAWNING, WAITING, COUNTING};
    
    //wave stats
    [SerializeField] private Wave[] wave;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountdown = 0;

    //bool
    [SerializeField] private bool isInfinite;

    private SpawnState state = SpawnState.COUNTING;

    private int currentWave;

    [SerializeField] private Text currentWaveText;
    [SerializeField] private Text currentZombieText;
    
    //references
    [SerializeField] private Transform[] spawner;
    [SerializeField] private List<CharacterStats> enemyList;
    [SerializeField] private GameObject charachter;

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemiesDead())
                return;
            else
            {
                FinishWave();
                int currentWaveNumber = currentWave += 1;
                currentWaveText.text = currentWaveNumber.ToString();
            }
        }
        
        if (waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(wave[currentWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        currentWave = 0;
    }

    private void ZombieCounter()
    {
        currentZombieText.text = enemyList.Count.ToString();
    }

    private IEnumerator SpawnWave(Wave wave)
    {
        state = SpawnState.SPAWNING;

        if (!isInfinite)
        {
            for (int i = 0; i < wave.numberOfEnemies; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(wave.delay);
            }

            state = SpawnState.WAITING;

            yield break;
        }
        
    }

    private void SpawnEnemy(GameObject enemy)
    {
        int randomInt = Random.RandomRange(1, spawner.Length);
        Transform randomSpawner = spawner[randomInt];

        GameObject spawnedEnemy = Instantiate(enemy, randomSpawner.position, randomSpawner.rotation);
        CharacterStats spawnedEnemyStats = spawnedEnemy.GetComponent<CharacterStats>();

        enemyList.Add(spawnedEnemyStats);
    }

    private bool EnemiesDead()
    {
        int i = 0;
        foreach(CharacterStats enemy in enemyList)
        {
            if (enemy.IsDead())
                i++;
            else
                return false;
        }
        return true;
    }

    private void FinishWave()
    {
        if (!isInfinite)
        {
            Debug.Log("Wave Finished");

            state = SpawnState.COUNTING;
            waveCountdown = timeBetweenWaves;

            if (currentWave + 1 > wave.Length - 1)
            {
                currentWave = 0;
                Debug.Log("All waves finished");
            }

            else
            {
                currentWave++;
            }
        }
    }
}
