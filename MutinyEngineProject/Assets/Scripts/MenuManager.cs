using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public GameObject mainMenu;
	public GameObject teamSelect;
	public GameObject[] readyMarks;
	public GameObject[] playerIcons;
	public GameObject[] playerPos;
	int numPlayers;

	int index = 0;

	bool teamScreen = false;


	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {

		if(teamScreen){			
			if (Input.GetButtonDown("Cancel")){
				
				MainMenu();

			}
		}
	}

	public void TeamSelect(){

		numPlayers = Input.GetJoystickNames().Length;

		playerIcons = new GameObject[numPlayers];
		readyMarks = new GameObject[numPlayers];

		teamSelect.SetActive(true);
		mainMenu.SetActive(false);

		SpawnPlayers();

		teamScreen = true;

	}

	public void MainMenu(){

		mainMenu.SetActive(true);
		teamSelect.SetActive(false);
		teamScreen = false;

	}


	public void StartGame(){

		if (ValidStart()){
			SceneManager.LoadScene("bake sale");
		}


	}

	bool ValidStart(){

		foreach (GameObject mark in readyMarks){

			if (!mark.activeSelf){
				return false;
			}
		}

		return true;
	}

	void SpawnPlayers(){

		index = 0;

		foreach (GameObject player in playerIcons){

			GameObject newPlayer = Instantiate(player, playerPos[index].transform.position, Quaternion.identity) as GameObject;
			playerIcons[index] = newPlayer;
			readyMarks[index] = newPlayer.GetComponentInChildren<GameObject>();
			readyMarks[index].gameObject.SetActive(false);
			index += 1;

		}
	}
		
}
