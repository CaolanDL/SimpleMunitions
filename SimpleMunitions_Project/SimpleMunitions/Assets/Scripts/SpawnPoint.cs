using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject gameWorld;

    private void Start()
    {
        gameWorld = GameObject.Find("GameWorld");
    }

    public void spawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, transform.position, transform.rotation, gameWorld.transform);
    }
}
