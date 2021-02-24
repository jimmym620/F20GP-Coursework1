using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    [SerializeField] Text killCounterText;
    public int killCount;

    [SerializeField] Text HealthDisplay;
    public int Health = 100;

    [SerializeField] Text EnemiesDisplay;
    public int enemiesRemaining;

    // Used in PlayerMovement and both gun scripts for detecting when each boost is on

    [SerializeField] Text WeaponBoostDisplay;
    [SerializeField] Text SpeedBoostDisplay;

    public bool WeaponBoostActive;
    public bool speedBoostActive;

    private void Awake()
    {
        // In case another instance of this exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Hide boost displays on game start
    void Start()
    {
        SpeedBoostDisplay.gameObject.SetActive(false);
        WeaponBoostDisplay.gameObject.SetActive(false);
    }

    void Update()
    {
        //UI FOR SHOWING ACTIVE BOOSTS
        if (speedBoostActive == true)
        {
            SpeedBoostDisplay.gameObject.SetActive(true);
        }
        if (WeaponBoostActive == true)
        {
            WeaponBoostDisplay.gameObject.SetActive(true);
        }

        if (speedBoostActive == false)
        {
            SpeedBoostDisplay.gameObject.SetActive(false);
        }
        if (WeaponBoostActive == false)
        {
            WeaponBoostDisplay.gameObject.SetActive(false);
        }

    }

    // Updating the displays 
    public void UpdateKillCount()
    {
        killCounterText.text = "Kills: " + killCount.ToString();
    }

    public void UpdateHealthDisplay()
    {
        HealthDisplay.text = "Health: " + Health.ToString();
    }

    public void UpdateEnemiesRemaining()
    {
        EnemiesDisplay.text = "Enemies Left: " + enemiesRemaining.ToString();
    }

}
