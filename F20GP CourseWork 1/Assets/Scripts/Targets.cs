using UnityEngine;

// Used in enemy Grunts and Brutes
public class Targets : MonoBehaviour
{
    public float health = 50f;

    // Take damage when hit by guns - shotgun and smg
    // If health at 0, enemy dies
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    
    // Update UI displays when enemy dies
    // Destroy the game object on death
    void Die()
    {
        UI_Manager.instance.killCount++;
        UI_Manager.instance.UpdateKillCount();

        UI_Manager.instance.enemiesRemaining--;
        UI_Manager.instance.UpdateEnemiesRemaining();
        Destroy(gameObject);
    }

}
