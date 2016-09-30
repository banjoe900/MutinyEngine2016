using UnityEngine;
using System.Collections;

public class RoundManager : MonoBehaviour {
    public int numberOfRounds = 5;
    private int currentRound;
    private int blueWins;
    private int orangeWins;

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
        currentRound = 1;
        blueWins = 0;
        orangeWins = 0;

        setPlayersAlive();
        storeStartingPositions();

        uiManager = GameObject.FindGameObjectWithTag("ui").GetComponent<UiManager>();
        uiManager.changeRoundNumber(currentRound);
        uiManager.changeBlueWins(blueWins);
        uiManager.changeBlueWins(orangeWins);
    }

    private void newRound(string winningTeam) {
        if (currentRound != numberOfRounds) {
            //stop all player movement
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
            //remove any items players are holding
            //destroy any projectiles that are left
            //reset stalls

            //chose rules for next round
            //now start the next round
            //enable all player movement
        } else { //that was the last round pick the winner

        }

    }

    /// <summary>
    /// sends all the players back to their inital starting positions
    /// </summary>
    private void resetPlayerPositions() {
        player1.transform.rotation = Quaternion.identity;
        player2.transform.rotation = Quaternion.identity;
        player3.transform.rotation = Quaternion.identity;
        player4.transform.rotation = Quaternion.identity;

        player1.transform.position = player1StartingPosition;
        player2.transform.position = player2StartingPosition;
        player3.transform.position = player3StartingPosition;
        player4.transform.position = player4StartingPosition;
        
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
}
