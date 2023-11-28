using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Threading;

public class SpawnPlayersScript : MonoBehaviour
{
    public GameObject player;
    public float posX, posY;

    public GameObject enemy;
    bool enemiesSpawned = false;
    public GameObject enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 position = new Vector2(UnityEngine.Random.Range(posX-2.5f, posX+2.5f), posY);
        /*int milliseconds = 2000;
        Thread.Sleep(milliseconds);*/
        PhotonNetwork.Instantiate (player.name, position, Quaternion.identity);
        // SpawnEnemies();
        // enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        // enemySpawner.
    }

    void SpawnEnemies()
    {
        
        Vector2 position = new Vector2(-2.5f, -2f);
        if (!enemiesSpawned)
        {
            PhotonNetwork.Instantiate(enemy.name, position, Quaternion.identity);
            enemiesSpawned = true;
        }
        
    }
}
