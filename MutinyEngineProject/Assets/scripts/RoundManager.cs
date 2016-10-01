using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {
    public GameObject uiPrefab;
    public GameObject playerPrefab;
    private List<int> blueTeam;
    private List<int> orangeTeam;

    //private Vector3[] startingPositions = {new Vector3(-3, -13, 21), new Vector3(9, -13, 21), new Vector3(8, -13, 13), new Vector3(-1, -13, 13)};
    private Vector3[] startingPositions = { new Vector3(20 ,1, 0), new Vector3(15, 1, 0), new Vector3(10, 1, 0), new Vector3(5, 1, 0)};
    private Vector3[] uiOrangePlayersPositions = {new Vector3(-820, -400, 0), new Vector3(-500, -400, 0)};
    private Vector3[] uiBluePlayersPositions = {new Vector3(490, -400, 0), new Vector3(810, -400, 0)};

    public int numberOfRounds = 5;
    private int currentRound;
    private int blueWins;
    private int orangeWins;

    public ControllerSpin controllerSpin;

    private List<GameObject> blueTeamPlayers = new List<GameObject>();
    private List<GameObject> orangeTeamPlayers = new List<GameObject>();

    //team 1
    public GameObject player1;
    private Vector3 player1StartingPosition;
    private bool player1Alive;

    public GameObject player2;
    private bool player2Alive;
    private Vector3 player2StartingPosition;

    //team 2
    public GameObject player3;
    private bool player3Alive;
    private Vector3 player3StartingPosition;

    public GameObject player4;
    private bool player4Alive;
    private Vector3 player4StartingPosition;


    private UiManager uiManager;

    // Use this for initialization
    void Start () {
        init();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void init() {
        blueTeam = GameObject.FindGameObjectWithTag("menuManager").GetComponent<MenuManager>().blueTeamMembers;
        orangeTeam = GameObject.FindGameObjectWithTag("menuManager").GetComponent<MenuManager>().orangeTeamMembers;
        SceneManager.UnloadScene(0);

        createPlayers();

        currentRound = 1;
        blueWins = 0;
        orangeWins = 0;

        //setPlayersAlive();
        //storeStartingPositions();

        uiManager = GameObject.FindGameObjectWithTag("ui").GetComponent<UiManager>();
        uiManager.changeRoundNumber(currentRound);
        uiManager.changeBlueWins(blueWins);
        uiManager.changeBlueWins(orangeWins);
    }

    private void newRound(string winningTeam) {
        if (currentRound != numberOfRounds) {
            currentRound++;
            uiManager.changeRoundNumber(currentRound);
            //change ui depending on what team won
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


            setPlayersAlive();
            resetPlayerPositions();
            resetPlayersSugarLevel();
            resetUi();
            
            //remove any items players are holding
            destroyAllProjectiles();

            //reset stalls

            //chose rules for next round
            randomiseControllerOrientation();
            //display mid round screen?
            //now start the next round
            enablePlayerMovements();
        } else { //that was the last round pick the winner
			if(blueWins > orangeWins){
				DisableControls();
				uiManager.blueWinScreen.SetActive(true);
			} else {
				DisableControls();
				uiManager.orangeWinScreen.SetActive(true);
			}
        }
    }

	public void DisableControls(){
		foreach (GameObject player in blueTeamPlayers)
		{
			player.GetComponent<PlayerMovement>().isEnabled = false;
		}
		foreach (GameObject player in orangeTeamPlayers)
		{
			player.GetComponent<PlayerMovement>().isEnabled = false;
		}
	}


    private void createPlayers() {
        //for loop for blue players
        for (int i = 0; i < blueTeam.Count; i++) {
            GameObject newUi = Instantiate(uiPrefab, new Vector3(0, 0, 0),  Camera.main.transform.rotation) as GameObject;
            newUi.transform.SetParent(GameObject.FindGameObjectWithTag("ui").transform);
            newUi.transform.localPosition = uiBluePlayersPositions[i];

            GameObject newPlayer = Instantiate(playerPrefab, startingPositions[i], Quaternion.identity) as GameObject;
            newPlayer.transform.SetParent(GameObject.Find("players").transform);
            newPlayer.GetComponent<PlayerMovement>().playerNumber = blueTeam[i];
            newPlayer.GetComponent<PlayerBehavior>().uiPlayerPort = newUi.transform.GetChild(0).GetComponent<Image>();
            newPlayer.GetComponent<PlayerBehavior>().uiPlayerName = newUi.transform.GetChild(1).GetComponent<Text>();
            newPlayer.GetComponent<PlayerBehavior>().uiPlayerSugarLevel = newUi.transform.GetChild(2).GetComponent<Text>();
            newPlayer.GetComponent<PlayerBehavior>().updateUi();
            newPlayer.GetComponent<PlayerBehavior>().changePortColour("blue");

            blueTeamPlayers.Add(newPlayer);


        }
        //for loop for orange players
        for (int i = 0; i < orangeTeam.Count; i++) {
            GameObject newUi = Instantiate(uiPrefab, new Vector3(0, 0, 0), Camera.main.transform.rotation) as GameObject;
            newUi.transform.SetParent(GameObject.FindGameObjectWithTag("ui").transform);
            newUi.transform.localPosition = uiOrangePlayersPositions[i];

            GameObject newPlayer = Instantiate(playerPrefab, startingPositions[i], Quaternion.identity) as GameObject;
            newPlayer.transform.SetParent(GameObject.Find("players").transform);
            newPlayer.GetComponent<PlayerMovement>().playerNumber = orangeTeam[i];
            newPlayer.GetComponent<PlayerBehavior>().uiPlayerPort = newUi.transform.GetChild(0).GetComponent<Image>();
            newPlayer.GetComponent<PlayerBehavior>().uiPlayerName = newUi.transform.GetChild(1).GetComponent<Text>();
            newPlayer.GetComponent<PlayerBehavior>().uiPlayerSugarLevel = newUi.transform.GetChild(2).GetComponent<Text>();
            newPlayer.GetComponent<PlayerBehavior>().updateUi();
            newPlayer.GetComponent<PlayerBehavior>().changePortColour("orange");
            orangeTeamPlayers.Add(newPlayer);
        }
    }


    /// <summary>
    /// sends all the players back to their inital starting positions
    /// </summary>
    private void resetPlayerPositions() {
        player1.transform.position = player1StartingPosition;
        player2.transform.position = player2StartingPosition;
        player3.transform.position = player3StartingPosition;
        player4.transform.position = player4StartingPosition;
        player1.transform.rotation = Quaternion.Euler(0, 0, 0);
        player2.transform.rotation = Quaternion.Euler(0, 0, 0);
        player3.transform.rotation = Quaternion.Euler(0, 0, 0);
        player4.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    /// <summary>
    /// takes the initial positions of all the players so that they can be sent back there each time a new round starts
    /// </summary>
    private void storeStartingPositions() {
        player1StartingPosition = player1.transform.position;
        player2StartingPosition = player2.transform.position;
        player3StartingPosition = player3.transform.position;
        player4StartingPosition = player4.transform.position;
    }

    public void killPlayer(int playerNumber) {
        switch(playerNumber) {
            case 1:
                player1Alive = false;
                if (player2Alive == false) {
                    newRound("orange");
                }
                break;
            case 2:
                player2Alive = false;
                if (player1Alive == false) {
                    newRound("orange");
                }
                break;
            case 3:
                player3Alive = false;
                if (player4Alive == false) {
                    newRound("blue");
                }
                break;
            case 4:
                player4Alive = false;
                if (player3Alive == false) {
                    newRound("blue");
                }
                break;
        }
    }

    /// <summary>
    /// sets all the players to alive
    /// </summary>
    private void setPlayersAlive() {
        player1Alive = true;
        player2Alive = true;
        player3Alive = true;
        player4Alive = true;
    }

    /// <summary>
    /// sets the sugar levels of all the players to 0
    /// </summary>
    private void resetPlayersSugarLevel() {
        player1.GetComponent<PlayerBehavior>().sugarLevel = 0;
        player2.GetComponent<PlayerBehavior>().sugarLevel = 0;
        player3.GetComponent<PlayerBehavior>().sugarLevel = 0;
        player4.GetComponent<PlayerBehavior>().sugarLevel = 0;
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

    /// <summary>
    /// enables the movement of all players
    /// </summary>
    private void enablePlayerMovements() {
        player1.GetComponent<PlayerMovement>().isEnabled = true;
        player2.GetComponent<PlayerMovement>().isEnabled = true;
        player3.GetComponent<PlayerMovement>().isEnabled = true;
        player4.GetComponent<PlayerMovement>().isEnabled = true;
    }

    private void resetUi() {
        player1.GetComponent<PlayerBehavior>().updateUi();
        player2.GetComponent<PlayerBehavior>().updateUi();
        player3.GetComponent<PlayerBehavior>().updateUi();
        player4.GetComponent<PlayerBehavior>().updateUi();
    }
    
    private void randomiseControllerOrientation()
    {
        int randomInt = Random.Range(0, 4);
        if (randomInt == (int)player1.GetComponent<PlayerMovement>().currentOrientation)
        {
            randomInt++;
            if (randomInt > 3) randomInt = 2;
        }
        PlayerMovement.ControllerOrientation orientation = (PlayerMovement.ControllerOrientation)randomInt;
        player1.GetComponent<PlayerMovement>().currentOrientation = orientation;
        player2.GetComponent<PlayerMovement>().currentOrientation = orientation;
        player3.GetComponent<PlayerMovement>().currentOrientation = orientation;
        player4.GetComponent<PlayerMovement>().currentOrientation = orientation;
        controllerSpin = FindObjectOfType<ControllerSpin>();
        if(controllerSpin != null) controllerSpin.SetOrientation(orientation);
    }
}
