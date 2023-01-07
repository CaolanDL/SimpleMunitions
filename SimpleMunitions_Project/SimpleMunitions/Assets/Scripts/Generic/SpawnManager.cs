using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] SpawnPool;
    public GameObject[] SpawnPoints;

    public GameManager gameManager;

    private Sequence spawnSequence;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        checkWaveComplete();
    }


    //Start a sequence to spawn enemies every [TimeInterval]
    private float startSpawnInterval = 2.7f;
    private float spawnIntervalChange = 0.75f;
    private int spawnCountChange = 3;

    private Sequence startSpawnLoop()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startSpawnInterval);
        sequence.AppendCallback(() => SpawnRandomEnemy());
        sequence.SetLoops(-1, LoopType.Restart);

        numToSpawn += spawnCountChange;

        numEKilled = 0; numESpawned = 0;

        return sequence;
    }

    void SpawnRandomEnemy()
    {
        //Pick a random spawnpoint and spawn a random enemy
        //Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform;
        SpawnPoint spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].GetComponent<SpawnPoint>();
        GameObject enemytospawn = SpawnPool[Random.Range(0, SpawnPool.Length)];

        spawnPoint.spawnEnemy(enemytospawn);

        numESpawned++;   //Add one to the number of spawned enemies
    }

    //Wave Control
    private int numToSpawn = 0;

    public int numEKilled = 0;
    private int numESpawned = 0;

    private void checkWaveComplete()
    {
        if (!gameManager.ShopActive)
        {
            //Stop spawning when numToSpawn reached
            if (numESpawned >= numToSpawn && spawnSequence.IsActive() )
            {
                spawnSequence.Pause();
                spawnSequence.Kill();
            }
            if (numEKilled >= numToSpawn)
            {
                gameManager.GotoShop();
            }
        }
    }

    public void enemyKilled()
    {
        numEKilled++;
    }

    public void startNewWave()
    {
        startSpawnInterval *= spawnIntervalChange;
        spawnSequence = startSpawnLoop();
    }
}
