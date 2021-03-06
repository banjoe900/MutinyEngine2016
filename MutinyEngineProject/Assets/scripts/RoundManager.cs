﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
    public GameObject uiPrefab;
    public GameObject playerPrefab;
    private List<int> blueTeam;
    private List<int> orangeTeam;

    //private Vector3[] startingPositions = {new Vector3(-3, -13, 21), new Vector3(9, -13, 21), new Vector3(8, -13, 13), new Vector3(-1, -13, 13)};
    private Vector3[] blueStartingPositions = { new Vector3(7 ,1, 0), new Vector3(7, 1, -6), };
    private Vector3[] orangeStartingPositions = { new Vector3(-6, 1, -6), new Vector3(-6, 1, 0) };
    private Vector3[] uiOrangePlayersPositions = {new Vector3(-820, -400, 0), new Vector3(-500, -400, 0)};
    private Vector3[] uiBluePlayersPositions = {new Vector3(490, -400, 0), new Vector3(810, -400, 0)};

    public int numberOfRounds = 5;
    private int currentRound;
    private int blueWins;
    private int orangeWins;

    public ControllerSpin controllerSpin;

    private List<GameObject> blueTeamPlayers = new List<GameObject>();
    private List<GameObject> orangeTeamPlayers = new List<GameObject>();

    private UiManager uiManager;

    private int blueDeaths;
    private int orangeDeaths;

	public bool winScreen = false;

    private PieSpawner pieSpawner;

    public Sprite bluePort;
    public Sprite orangePort;

    private float timer = 0;

    // Use this for initialization
    void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {

		if (winScreen){
            timer += Time.deltaTime;
            if (timer > 2)
            {
                if (Input.GetButtonDown("Submit"))
                {
                    SceneManager.LoadScene(0);
                }
            }
		}
	
	}

    private void init() {
        blueTeam = GameObject.FindGameObjectWithTag("menuManager").GetComponent<MenuManager>().blueTeamMembers;
        orangeTeam = GameObject.FindGameObjectWithTag("menuManager").GetComponent<MenuManager>().orangeTeamMembers;
        SceneManager.UnloadScene(0);
        pieSpawner = FindObjectOfType<PieSpawner>();

        createPlayers();

        pieSpawner.isEnabled = true;
        pieSpawner.spawnRate = 12f;

        currentRound = 1;
        blueWins = 0;
        orangeWins = 0;
        blueDeaths = 0;
        orangeDeaths = 0;

        uiManager = GameObject.FindGameObjectWithTag("ui").GetComponent<UiManager>();
        uiManager.changeBlueWins(blueWins);
        uiManager.changeBlueWins(orangeWins);
    }

    private void newRound(string winningTeam) {

        if (winningTeam == "blue") {

            blueWins++;
            uiManager.changeBlueWins(blueWins);
        }
        else if (winningTeam == "orange") {

            orangeWins++;
            uiManager.changeOrangeWins(orangeWins);
        }
        else {
            Debug.Log("hey stupid, the teams are blue and orange");
        }
        if (blueWins == 2 || orangeWins == 2) {
            GameObject.Find("music").GetComponent<AudioSource>().enabled = false;
            GameObject.Find("lastRoundMusic").GetComponent<AudioSource>().enabled = true;
        }
        pieSpawner.isEnabled = false;
        pieSpawner.spawnRate -= 2f;
        if (currentRound != numberOfRounds && blueWins < 3 && orangeWins < 3) {
            
            currentRound++;
			
            orangeDeaths = 0;
            blueDeaths = 0;

            resetPlayerPositions();

			//Reset the players
            resetPlayersSugarLevel();

			//Reset the screen
            resetUi();

			//Reset the level
            destroyAllProjectiles();

			//Reset the default controls
            randomiseControllerOrientation();
            pieSpawner.isEnabled = true;
            for(int i = 0; i < blueTeamPlayers.Count; i++) {
                blueTeamPlayers[i].GetComponent<Player_Projectiles>().canPickup = false;
                blueTeamPlayers[i].GetComponent<PlayerBehavior>().canBeHit = true;
                blueTeamPlayers[i].GetComponent<PlayerMovement>().isEnabled = true;
            }
            for (int i = 0; i < orangeTeamPlayers.Count; i++) {
                orangeTeamPlayers[i].GetComponent<Player_Projectiles>().canPickup = false;
                orangeTeamPlayers[i].GetComponent<PlayerBehavior>().canBeHit = true;
                orangeTeamPlayers[i].GetComponent<PlayerMovement>().isEnabled = true;
            }

        } else { //that was the last round pick the winner
            pieSpawner.isEnabled = false;
            if (blueWins > orangeWins){
				DisableMovement();
				uiManager.blueWinScreen.SetActive(true);
				winScreen = true;
				Debug.Log("Blue wins");
			} else {
				DisableMovement();
				uiManager.orangeWinScreen.SetActive(true);
				winScreen = true;
				Debug.Log("orange wins");
			}				
        }
    }

	public void DisableMovement(){
		foreach (GameObject player in blueTeamPlayers)
		{
			player.GetComponent<PlayerMovement>().isEnabled = false;
		}
		foreach (GameObject player in orangeTeamPlayers)
		{
			player.GetComponent<PlayerMovement>().isEnabled = false;
		}
	}

	public void EnableMovement(){
		foreach (GameObject player in blueTeamPlayers)
		{
			player.GetComponent<PlayerMovement>().isEnabled = true;
		}
		foreach (GameObject player in orangeTeamPlayers)
		{
			player.GetComponent<PlayerMovement>().isEnabled = true;
		}
	}

    private void createPlayers() {
        //for loop for blue players
        for (int i = 0; i < blueTeam.Count; i++) {
            GameObject newUi = Instantiate(uiPrefab, new Vector3(0, 0, 0),  Camera.main.transform.rotation) as GameObject;
            newUi.transform.SetParent(GameObject.FindGameObjectWithTag("ui").transform);
            newUi.transform.localPosition = uiBluePlayersPositions[i];

            GameObject newPlayer = Instantiate(playerPrefab, blueStartingPositions[i], Quaternion.identity) as GameObject;
            newPlayer.transform.SetParent(GameObject.Find("players").transform);
            newPlayer.transform.GetChild(5).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = Resources.Load("grandma_blue") as Material;

            PlayerMovement playerMovement = newPlayer.GetComponent<PlayerMovement>();
            PlayerBehavior playerBehavior = newPlayer.GetComponent<PlayerBehavior>();
            playerMovement.playerNumber = blueTeam[i];
            playerBehavior.uiPlayerPort = newUi.transform.GetChild(0).GetComponent<Image>();
            //Texture te Resources.Load("Portrait - Blue Team") as Texture
            //Sprite sprite = Sprite.Create(new Texture2D(128, 128), new Rect(0, 0, 128, 128), new Vector2(0, 0));
            playerBehavior.uiPlayerPort.sprite = bluePort;
            playerBehavior.uiPlayerName = newUi.transform.GetChild(1).GetComponent<Text>();
            playerBehavior.uiPlayerSugarLevel = newUi.transform.GetChild(2).GetComponent<Slider>();
            playerBehavior.updateUi();

            blueTeamPlayers.Add(newPlayer);


        }
        //for loop for orange players
        for (int i = 0; i < orangeTeam.Count; i++) {
            GameObject newUi = Instantiate(uiPrefab, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
            newUi.transform.SetParent(GameObject.FindGameObjectWithTag("ui").transform);
            newUi.transform.localPosition = uiOrangePlayersPositions[i];

            GameObject newPlayer = Instantiate(playerPrefab, orangeStartingPositions[i], Quaternion.identity) as GameObject;
            newPlayer.transform.SetParent(GameObject.Find("players").transform);
            newPlayer.transform.GetChild(5).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = Resources.Load("grandma_orange") as Material;

            PlayerMovement playerMovement = newPlayer.GetComponent<PlayerMovement>();
            PlayerBehavior playerBehavior = newPlayer.GetComponent<PlayerBehavior>();

            playerMovement.playerNumber = orangeTeam[i];
            playerBehavior.uiPlayerPort = newUi.transform.GetChild(0).GetComponent<Image>();
            playerBehavior.uiPlayerPort.sprite = orangePort;
            playerBehavior.uiPlayerName = newUi.transform.GetChild(1).GetComponent<Text>();
            playerBehavior.uiPlayerSugarLevel = newUi.transform.GetChild(2).GetComponent<Slider>();
            playerBehavior.updateUi();
            orangeTeamPlayers.Add(newPlayer);
        }
    }


    /// <summary>
    /// sends all the players back to their inital starting positions
    /// </summary>
    private void resetPlayerPositions() {
        for (int i = 0; i < blueTeamPlayers.Count; i++) {
            blueTeamPlayers[i].transform.position = blueStartingPositions[i];
            blueTeamPlayers[i].transform.rotation = Quaternion.identity;
        }
        for (int i = 0; i < orangeTeamPlayers.Count; i++) {
            orangeTeamPlayers[i].transform.position = orangeStartingPositions[i];
            orangeTeamPlayers[i].transform.rotation = Quaternion.identity;
        }

    }

    public void killPlayer(int playerNumber) {
        for (int i = 0; i < blueTeamPlayers.Count; i++) {
            if (blueTeamPlayers[i].GetComponent<PlayerMovement>().playerNumber == playerNumber) {
                if(blueTeamPlayers[i].GetComponent<PlayerBehavior>().canBeHit == true) {
                    blueTeamPlayers[i].GetComponent<PlayerBehavior>().canBeHit = false;
                    blueTeamPlayers[i].GetComponent<Player_Projectiles>().Player_Ammo = 0;
                    blueTeamPlayers[i].GetComponent<Player_Projectiles>().canPickup = false;
                    blueTeamPlayers[i].GetComponent<PlayerMovement>().isEnabled = false;
                    blueDeaths++;
                    i = blueTeamPlayers.Count;
                }

            }
        }

        for (int i = 0; i < orangeTeamPlayers.Count; i++) {
            if (orangeTeamPlayers[i].GetComponent<PlayerMovement>().playerNumber == playerNumber) {
                if (orangeTeamPlayers[i].GetComponent<PlayerBehavior>().canBeHit == true) {
                    orangeDeaths++;
                    orangeTeamPlayers[i].GetComponent<PlayerBehavior>().canBeHit = false;
                    orangeTeamPlayers[i].GetComponent<Player_Projectiles>().Player_Ammo = 0;
                    orangeTeamPlayers[i].GetComponent<Player_Projectiles>().canPickup = false;
                    orangeTeamPlayers[i].GetComponent<PlayerMovement>().isEnabled = false;
                    i = orangeTeamPlayers.Count;
                }
            }
        }


        if (blueDeaths >= blueTeamPlayers.Count) {
            newRound("orange");
        } else if (orangeDeaths >= orangeTeamPlayers.Count) {
            newRound("blue");
        }
    }

    /// <summary>
    /// sets the sugar levels of all the players to 0
    /// </summary>
    private void resetPlayersSugarLevel() {
        for (int i = 0; i < blueTeamPlayers.Count; i++) {
            PlayerBehavior player = blueTeamPlayers[i].GetComponent<PlayerBehavior>();
            player.sugarLevel = 0;
            player.updateUi();
        }
        for (int i = 0; i < orangeTeamPlayers.Count; i++) {
            PlayerBehavior player = orangeTeamPlayers[i].GetComponent<PlayerBehavior>();
            player.sugarLevel = 0;
            player.updateUi();
        }
    }

    /// <summary>
    /// deletes all objects in the scene with the tag projectile
    /// </summary>
    private void destroyAllProjectiles() {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("projectile");
        for (int i = 0; i < projectiles.Length; i++) {
            Destroy(projectiles[i]);
        }
    }

    private void resetUi() {
        for (int i = 0; i < blueTeamPlayers.Count; i++) {
            blueTeamPlayers[i].GetComponent<PlayerBehavior>().updateUi();
        }
        for (int i = 0; i < orangeTeamPlayers.Count; i++) {
            orangeTeamPlayers[i].GetComponent<PlayerBehavior>().updateUi();
        }
    }
    
    private void randomiseControllerOrientation()
    {
        int randomInt = Random.Range(0, 4);
        if (randomInt == (int)blueTeamPlayers[0].GetComponent<PlayerMovement>().currentOrientation)
        {
            randomInt++;
            if (randomInt > 3) randomInt = 2;
        }
        PlayerMovement.ControllerOrientation orientation = (PlayerMovement.ControllerOrientation)randomInt;
        for (int i = 0; i < blueTeamPlayers.Count; i++) {
            blueTeamPlayers[i].GetComponent<PlayerMovement>().currentOrientation = orientation;
        }
        for (int i = 0; i < orangeTeamPlayers.Count; i++) {
            orangeTeamPlayers[i].GetComponent<PlayerMovement>().currentOrientation = orientation;
        }
        controllerSpin = FindObjectOfType<ControllerSpin>();
        if(controllerSpin != null) controllerSpin.SetOrientation(orientation);
    }
}
