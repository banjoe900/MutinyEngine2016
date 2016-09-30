using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public static MenuManager _instance;
	public static MenuManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<MenuManager>();
			}
			return _instance;
			
		}
	}


	public GameObject mainMenu;
	public GameObject teamSelect;
	public GameObject[] playerIcons;
	public GameObject[] playerPos;
	public GameObject playerIcon;
	int numPlayers;
	public int orangeTeam = 0;
	public int tealTeam = 0;

	bool teamScreen = false;


	// Use this for initialization
	void Start () {
	


	}
	
	// Update is called once per frame
	void Update () {

		if(teamScreen){

			if (Input.GetButtonDown("Cancel")){
				foreach (GameObject icon in playerIcons)
				{
					Destroy(icon);
				}
				orangeTeam = 0;
				tealTeam= 0;
				MainMenu();

			}
			if(Input.GetButtonDown("Submit")){

				/*foreach (GameObject icon in playerIcons)
				{
					ADD PLAYERS TO GAME MANAGER TEAM ARRAYS
					if (icon.GetComponent<MenuPlayer>().isOrange){
							gameManager.orangeTeam.Add(player);
					}
				}*/

				StartGame();
			}
		}
	}

	public void TeamSelect(){

		numPlayers = Input.GetJoystickNames().Length;

		playerIcons = new GameObject[numPlayers];

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
			//SceneManager.LoadScene("bake sale");
			Debug.Log("LOAD LEVEL");
		}


	}

	bool ValidStart(){

		foreach (GameObject icon in playerIcons){

			if (!icon.GetComponent<MenuPlayer>().playerReady){
				return false;
			}
		}

		return true;
	}

	void SpawnPlayers(){

		for (int i = 0; i < playerIcons.Length; i++){

			GameObject newPlayer = Instantiate(playerIcon, playerPos[i].transform.position, Quaternion.identity) as GameObject;
			newPlayer.transform.SetParent(teamSelect.transform);
			newPlayer.GetComponent<MenuPlayer>().playerNumber = i+1;
			playerIcons[i] = newPlayer;
		}
	}	
}
