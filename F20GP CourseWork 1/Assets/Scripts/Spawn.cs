using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    // List of enemy types - currently 2, Grunt and Brute
    public GameObject[] enemyList;

    // Number of enemies to kill to beat the level - can be changed in inspector
    public int EnemiesToKill = 10;
    

    // Start spawning enemies on game start, show the number of enemies left to kill
    void Start()
    {
        if (EnemiesToKill != 0)
        {
            StartCoroutine("SpawnEnemy");
        }
        UI_Manager.instance.enemiesRemaining = EnemiesToKill +1;
        UI_Manager.instance.UpdateEnemiesRemaining();

    }

    //Get a random position out of 3 possible spawn locations (spots)
    Vector3 getRandomPosition()
    {
        
        float[] xCoord = new float[3];
        float[] zCoord = new float[3];

        //SPOT 1
        xCoord[0] = Random.Range(45, 61);
        zCoord[0] = Random.Range(-74, -40);
        //SPOT 2
        xCoord[1] = Random.Range(77, 118);
        zCoord[1] = Random.Range(-80, -58);
        //SPOT 3
        xCoord[2] = Random.Range(73, 136);
        zCoord[2] = Random.Range(-45, -33);

        // PICK FROM THE 3 SPOTS AND USE AS NEW VECTOR AS A SPAWN
        int spots = Random.Range(0, 2);
        float x = xCoord[spots];
        float z = zCoord[spots];

        float y = 2f;

        Vector3 newPosition = new Vector3(x, y, z);
        return newPosition;

        /*float x = Random.Range(79, 137);
        float z = Random.Range(-81, -32);
        float y = 2f;

        Vector3 newPosition = new Vector3(x, y, z);
        return newPosition;*/
    }

    // Spawn enemies infinitely until the coroutine is stopped, wait 3 seconds between each spawn
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemyList[Random.Range(0, 2)], getRandomPosition(), Quaternion.identity);
            EnemiesToKill--;
            yield return new WaitForSeconds(3);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // If player has killed the required amount of enemies, stop spawning enemies
        if (EnemiesToKill == 0)
        {
            StopCoroutine("SpawnEnemy");

        }

    }
}
