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


    //Start a pausable sequence to spawn enemies every [TimeInterval]
    private float startSpawnInterval = 1.5f;
    private float spawnIntervalChange = 0.8f;
    private Sequence startSpawnLoop()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startSpawnInterval);
        sequence.AppendCallback(() => SpawnRandomEnemy());
        sequence.SetLoops(-1, LoopType.Restart);

        return sequence;
    }

    void SpawnRandomEnemy()
    {
        //Pick a random spawnpoint and spawn a random enemy
        Transform spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform;
        Instantiate(SpawnPool[Random.Range(0, SpawnPool.Length)], spawnPoint.position, spawnPoint.rotation);

        numESpawned++;   //Add one to the number of spawned enemies
    }

    //Wave Control
    private int numToESpawn = 8;

    public int numEKilled = 0;
    private int numESpawned = 0;

    private void checkWaveComplete()
    {
        if (!gameManager.ShopActive)
        {
            if (numESpawned >= numToESpawn)
            {
                spawnSequence.Kill();
            }
            if (numEKilled >= numToESpawn)
            {
                gameManager.GotoShop();
            }
        }
    }

    public void startNewWave()
    {
        numToESpawn = 8;
        startSpawnInterval *= spawnIntervalChange;
        spawnSequence = startSpawnLoop();
    }
}
