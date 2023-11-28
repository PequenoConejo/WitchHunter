using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using System.Threading;

public class SpawnEnemiesScript : MonoBehaviour
{

    public GameObject enemy;
    bool enemiesSpawned = false;
    public const int enemyCount = 5;
    float[] coordX;
    float[] coordY;
    bool[] needPath;
    Platformer.Mechanics.PatrolPath[] enemyPaths;
    GameObject enemySpawned;

    public GameObject vicManagerToSpawn;
    GameObject vicManager;
    // Start is called before the first frame update
    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            setCoords();
            setPaths();

            if (!enemiesSpawned)
            {
                int k = 0;
                
                for (int i = 0;i < enemyCount; i++)
                {
                    Vector2 position = new Vector2(coordX[i], coordY[i]);
                    enemySpawned = (GameObject) PhotonNetwork.Instantiate(enemy.name, position, Quaternion.identity);
                    if (needPath[i])
                    {
                        Platformer.Mechanics.EnemyController controller = (Platformer.Mechanics.EnemyController)enemySpawned.GetComponent(typeof(Platformer.Mechanics.EnemyController));
                        controller.path = enemyPaths[k];
                        // controller.Path = enemyPaths[k];
                        Debug.Log("Enemy spawned: " + i + " // Enemy name: "+ enemySpawned.name + 
                            " // Enemy needPath: " + needPath[i] + " // Path: " + controller.path);
                    
                        /*Debug.Log(enemySpawned.name);
                        Debug.Log(controller.path);*/
                        
                        k++;
                    }

                }
                enemiesSpawned = true;
            }
            SetVictoryManager();
        }

    }

    void setCoords()
    {
        coordX = new float[enemyCount];
        coordY = new float[enemyCount];
        needPath= new bool[enemyCount];

        coordX[0] = -2f; coordY[0] = -2f; needPath[0] = false;
        coordX[1] = 7f; coordY[1] = -5f; needPath[1] = true;
        coordX[2] = 17.5f; coordY[2] = -3f; needPath[2] = true;
        coordX[3] = 38f; coordY[3] = -9f; needPath[3] = true;
        coordX[4] = 51.5f; coordY[4] = -3f; needPath[4] = false;
    }

    void setPaths()
    {
        enemyPaths = (Platformer.Mechanics.PatrolPath[]) FindObjectsOfType(typeof(Platformer.Mechanics.PatrolPath));
        // Array.Sort(enemyPaths);
        Platformer.Mechanics.PatrolPath temp;
        for (int i = 0; i < enemyPaths.Length - 1; i++)
        {
            for (int j = i + 1; j < enemyPaths.Length; j++)
            {
                if (String.Compare(enemyPaths[i].name, enemyPaths[j].name)>0)
                {
                    temp = enemyPaths[i];
                    enemyPaths[i] = enemyPaths[j];
                    enemyPaths[j] = temp;
                }
            }
        }
    }

    void SetVictoryManager()
    {
        Vector2 position = new Vector2(0f,0f);
        vicManager = (GameObject)PhotonNetwork.Instantiate("VictoryManager", position, Quaternion.identity);
        Debug.Log("Victory manager spawned");
    }

    
}
