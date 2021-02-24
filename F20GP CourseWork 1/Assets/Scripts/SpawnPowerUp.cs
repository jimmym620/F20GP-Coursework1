using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUp : MonoBehaviour
{
    public GameObject[] powerUpList; // 1 - health, 2 - speed, 3 - weapon boost

    //get a random position out of 3 possible spawn locations (spots)
    Vector3 getRandomPosition()
    {
        //
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

        // PICK FROM THE 3 SPOTS AND USE AS NEW VECTOR
        int spots = Random.Range(0, 2);
        float x = xCoord[spots];
        float z = zCoord[spots];

        float y = 2f;


        Vector3 newPosition = new Vector3(x, y, z);
        return newPosition;
    }

    // 20 secs between each health orb spawn
    IEnumerator SpawnHealth()
    {   
        yield return new WaitForSeconds(20);
        Instantiate(powerUpList[0], getRandomPosition(), Quaternion.identity);

    }

    // 10 secs between each speed orb spawn
    IEnumerator SpawnSpeed()
    {
        yield return new WaitForSeconds(10);
        Instantiate(powerUpList[1], getRandomPosition(), Quaternion.identity);

    }

    // 15 secs between each weapon boost orb spawn
    IEnumerator SpawnWeaponBoost()
    {
        yield return new WaitForSeconds(15);
        Instantiate(powerUpList[2], getRandomPosition(), Quaternion.identity);
    }


    void Update()
    {
        //DONT SPAWN MORE ORBS IF ONE IS PRESENT

        //health
        if (GameObject.FindGameObjectsWithTag("HealthOrb").Length == 0)
        {
            StartCoroutine("SpawnHealth");
        }
        if (GameObject.FindGameObjectsWithTag("HealthOrb").Length > 0)
        {
            StopCoroutine("SpawnHealth");
        }

        //speed
        if(GameObject.FindGameObjectsWithTag("SpeedOrb").Length == 0)
        {
            StartCoroutine("SpawnSpeed");
        }
        if (GameObject.FindGameObjectsWithTag("SpeedOrb").Length > 0)
        {
            StopCoroutine("SpawnSpeed");
        }

        //weapon boost
        if (GameObject.FindGameObjectsWithTag("WeaponBoostOrb").Length == 0)
        {
            StartCoroutine("SpawnWeaponBoost");
        }
        if (GameObject.FindGameObjectsWithTag("WeaponBoostOrb").Length > 0)
        {
            StopCoroutine("SpawnWeaponBoost");
        }
    }
}
