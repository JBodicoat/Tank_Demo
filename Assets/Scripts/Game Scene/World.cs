using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour {

    public GameObject player;
    public GameObject enemyTank;

    public GameObject pauseScreen;
    public GameObject pauseMenu;
    public GameObject keyBindingsMenu;

    public Transform deathCam;

    public Text scoreText;
    public Text healthText;

    private bool gameState = true;

    private int scoreValue = 0;
    private int stage = 0;

    private const int scoreIncrement = 500;

    private float healthValue = 100.0f;

    Vector3 deathCamDisposition = new Vector3(0, 10, 0);

    private const string menuButton = "escape";

	// Use this for initialization
	void Start () {
        
        deathCam.gameObject.SetActive(false);
        pauseScreen.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void LateUpdate () {
        //if the player does not exist game state is set to false and camera "deathCam" is turned on
        if (player.GetComponent<PlayerTankController>().getPlayerState() == false)
        {
            gameState = false;
            Destroy(scoreText);
            Destroy(healthText);
            deathCam.gameObject.SetActive(true);
        }
        else
        {
            deathCam.transform.position = player.transform.position + deathCamDisposition;
            healthValue = player.GetComponent<PlayerTankController>().GetHealth();
            scoreText.text = "Score: " + scoreValue;
            healthText.text = "Health: " + (int)healthValue;
            if (Input.GetKeyDown(menuButton))
            {
                PauseGame();
            }
        }
	}

    //returns the value of "gameState"
    public bool GetGameState()
    {
        return gameState;
    }

    //increases score when an enemy tank is destroyed
    public void IncrementScore()
    {
        scoreValue = scoreValue + scoreIncrement;
    }

    //spawns the next wave of enemies when all enemies destroyed
    public void SpawnNextStage()
    {
        stage++;
        switch (stage)
        {
            case 1:
                Instantiate(enemyTank, new Vector3(1, 0, 1), transform.rotation);
                Instantiate(enemyTank, new Vector3(10, 0, 10), transform.rotation);
                break;
            case 2:
                Instantiate(enemyTank, new Vector3(1, 0, 1), transform.rotation);
                Instantiate(enemyTank, new Vector3(10, 0, 10), transform.rotation);
                Instantiate(enemyTank, new Vector3(20, 0, 20), transform.rotation);
                break;
            case 3:
                Instantiate(enemyTank, new Vector3(1, 0, 1), transform.rotation);
                Instantiate(enemyTank, new Vector3(10, 0, 10), transform.rotation);
                Instantiate(enemyTank, new Vector3(20, 0, 20), transform.rotation);
                Instantiate(enemyTank, new Vector3(-10, 0, -10), transform.rotation);
                break;
            case 4:
                Instantiate(enemyTank, new Vector3(1, 0, 1), transform.rotation);
                Instantiate(enemyTank, new Vector3(10, 0, 10), transform.rotation);
                Instantiate(enemyTank, new Vector3(20, 0, 20), transform.rotation);
                Instantiate(enemyTank, new Vector3(-10, 0, -10), transform.rotation);
                Instantiate(enemyTank, new Vector3(-20, 0, -20), transform.rotation);
                break;
        }
    }

    //pauses the game and opens pause menu or the opoosite if the pause menu is already open
    public void PauseGame()
    {
        pauseScreen.gameObject.SetActive(!pauseScreen.gameObject.activeSelf);
        keyBindingsMenu.gameObject.SetActive(!pauseScreen.gameObject.activeSelf);
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    //opens and closes key bindings menu
    public void OpenKeyBindings()
    {
        pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        keyBindingsMenu.gameObject.SetActive(!keyBindingsMenu.gameObject.activeSelf);
    }

    //closes the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
