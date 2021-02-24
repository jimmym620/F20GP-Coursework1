using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbObtained : MonoBehaviour
{
    // If player collides into the orb, destroy the game object
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Despawn();
        }
    }
    void Despawn()
    {
        Destroy(gameObject);
    }
}
