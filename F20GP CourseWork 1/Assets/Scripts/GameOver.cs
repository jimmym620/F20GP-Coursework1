using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text GameOverLoseDisplay;
    public Text GameOverWinDisplay;
    public Text PressRestartDisplay;
    public Text DescriptionDisplay;
    bool gameOver;

    void Start()
    {
        // Displaying bits of UI on game start
        GameOverWinDisplay.gameObject.SetActive(false);
        GameOverLoseDisplay.gameObject.SetActive(false);
        PressRestartDisplay.gameObject.SetActive(false);
        DescriptionDisplay.gameObject.SetActive(true);
        gameOver = false;
    }
    void Update()
    {
        // Show and hide appropriate UI for each game ending condition

        // WIN CONDITION - PLAYER DEFEATED ALL ENEMIES
        if (UI_Manager.instance.enemiesRemaining == 0 && gameOver == false)
        {
            GameOverWinDisplay.gameObject.SetActive(true);
            PressRestartDisplay.gameObject.SetActive(true);
            DescriptionDisplay.gameObject.SetActive(false);
            gameOver = true;
        }

        // LOSE CONDITION - PLAYER DID NOT SURVIVE
        if (UI_Manager.instance.Health == 0 && gameOver == false)
        {
            GameOverLoseDisplay.gameObject.SetActive(true);
            PressRestartDisplay.gameObject.SetActive(true);
            DescriptionDisplay.gameObject.SetActive(false);
            gameOver = true;
        }

        // PRESS G TO RESTART GAME
        if (Input.GetKey("g"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

}
