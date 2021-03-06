﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public static MenuManager _instance;
    public static MenuManager instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<MenuManager>();
            }
            return _instance;

        }
    }

    public GameObject mainMenu;
    public GameObject teamSelect;

	public GameObject creditsPage;
	public GameObject Instructions;

	public MenuAudioController MAC;

    public GameObject[] playerIcons;
    public GameObject[] playerPos;
    public GameObject playerIcon;
    public Button playButton;
    int numPlayers = 0;
    public int orangeTeam = 0;
    public int tealTeam = 0;
	public int readiedPlayers;

	public Sprite[] spriteArray = new Sprite[4];

    bool teamScreen = false;
	bool creditScreen = false;

    public List<int> blueTeamMembers = new List<int>();
    public List<int> orangeTeamMembers = new List<int>();

	public GameObject[] iconPositions;
	public GameObject[] iconPositionsLeft;
	public GameObject[] iconPositionsRight;


    // Use this for initialization
    void Start() {



    }

    // Update is called once per frame
    void Update() {

        if (teamScreen) {

            if (Input.GetButtonDown("Cancel")) {
                foreach (GameObject icon in playerIcons) {
                    Destroy(icon);
                }
                orangeTeam = 0;
                tealTeam = 0;
                numPlayers = 0;
				blueTeamMembers.Clear();
				orangeTeamMembers.Clear();
				Instructions.SetActive(false);
                MainMenu();

            }
			if (readiedPlayers == numPlayers)
			{
				Instructions.SetActive(true);
				if (Input.GetButtonDown("Submit")) {
					MAC.PlayPositiveSound();
					StartGame();
				}
			}
            
        }
		if (creditScreen)
		{
			if (Input.GetButtonDown("Cancel")) {
				creditScreen = false;
				creditsPage.SetActive(false);
				MainMenu();
			}
		}
    }

    public void TeamSelect() {

		MAC.PlayPositiveSound();
        numPlayers = 0;

        //numPlayers = Input.GetJoystickNames().Length;
        foreach (string joystick in Input.GetJoystickNames()) {
            if (joystick != "") {
                numPlayers += 1;
            }
        }
        Debug.Log(numPlayers);

        playerIcons = new GameObject[numPlayers];

        teamSelect.SetActive(true);
        mainMenu.SetActive(false);

        SpawnPlayers();

        teamScreen = true;

    }

	public void Credits(){

		creditsPage.SetActive(true);
		creditScreen = true;
	}

    public void MainMenu() {
		
		MAC.PlayNegativeSound();
        playButton.Select();
        mainMenu.SetActive(true);
        teamSelect.SetActive(false);
        teamScreen = false;

    }


    public void StartGame() {

        if (ValidStart()) {
			MAC.PlayPositiveSound();
            SceneManager.LoadScene(1, LoadSceneMode.Additive);
            Debug.Log("LOAD LEVEL");
        }


    }

    bool ValidStart() {

        foreach (GameObject icon in playerIcons) {

            if (!icon.GetComponent<MenuPlayer>().playerReady) {
                return false;
            }
            if (tealTeam == 0 || orangeTeam == 0) {
                return false;
            }
        }

        return true;
    }

    void SpawnPlayers() {

        for (int i = 0; i < playerIcons.Length; i++) {

            GameObject newPlayer = Instantiate(playerIcon, playerPos[i].transform.position, Quaternion.identity) as GameObject;
            newPlayer.transform.SetParent(teamSelect.transform);
            newPlayer.GetComponent<MenuPlayer>().playerNumber = i + 1;
			newPlayer.GetComponent<Image>().sprite = spriteArray[i];
            playerIcons[i] = newPlayer;
        }
    }

	public void QuitGame(){
		Application.Quit();
	}



}
